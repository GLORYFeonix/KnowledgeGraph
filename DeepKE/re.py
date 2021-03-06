import os
import sys
import torch
import logging
import hydra
from hydra import utils
from deepke.relation_extraction.standard.tools import Serializer
from deepke.relation_extraction.standard.tools import _serialize_sentence, _convert_tokens_into_index, _add_pos_seq, _handle_relation_data , _lm_serialize
import matplotlib.pyplot as plt
sys.path.append(os.path.abspath(os.path.join(os.path.dirname(__file__), "../")))
from deepke.relation_extraction.standard.utils import load_pkl, load_csv
import deepke.relation_extraction.standard.models as models
import csv


logger = logging.getLogger(__name__)


def _preprocess_data(data, cfg):
    
    relation_data = load_csv(os.path.join(cfg.cwd, cfg.data_path, 'relation.csv'), verbose=False)
    rels = _handle_relation_data(relation_data)

    if cfg.model_name != 'lm':
        vocab = load_pkl(os.path.join(cfg.cwd, cfg.out_path, 'vocab.pkl'), verbose=False)
        cfg.vocab_size = vocab.count
        serializer = Serializer(do_chinese_split=cfg.chinese_split)
        serial = serializer.serialize

        _serialize_sentence(data, serial, cfg)
        _convert_tokens_into_index(data, vocab)
        _add_pos_seq(data, cfg)
        logger.info('start sentence preprocess...')
        formats = '\nsentence: {}\nchinese_split: {}\nreplace_entity_with_type:  {}\nreplace_entity_with_scope: {}\n' \
                'tokens:    {}\ntoken2idx: {}\nlength:    {}\nhead_idx:  {}\ntail_idx:  {}'
        # logger.info(
        #     formats.format(data[0]['sentence'], cfg.chinese_split, cfg.replace_entity_with_type,
        #                 cfg.replace_entity_with_scope, data[0]['tokens'], data[0]['token2idx'], data[0]['seq_len'],
        #                 data[0]['head_idx'], data[0]['tail_idx']))
    else:
        _lm_serialize(data,cfg)

    return data, rels


def _get_predict_instance(cfg,sentence,head,tail):
    head_type = ""
    tail_type = ""

    instance = dict()
    instance['sentence'] = sentence.strip()
    instance['head'] = head.strip()
    instance['tail'] = tail.strip()
    if head_type.strip() == '' or tail_type.strip() == '':
        cfg.replace_entity_with_type = False
        instance['head_type'] = 'None'
        instance['tail_type'] = 'None'
    else:
        instance['head_type'] = head_type.strip()
        instance['tail_type'] = tail_type.strip()

    return instance


def _load_ner_result():
    nodes=[]
    with open("C:/Users/gzy88/Desktop/KnowledgeGraph/API.DAL/NLP/data/single.txt",encoding="utf-8") as f:
        sentence=f.readline().strip()
    with open("C:/Users/gzy88/Desktop/KnowledgeGraph/API.DAL/NLP/data/ner_result.csv", mode="r", encoding="utf-8", newline="") as f:
        reader = csv.DictReader(f)
        for row in reader:
            nodes.append(row["Node"])

    return nodes, sentence


def _eval_relationship(cfg,data,rels,device,model):
    x = dict()
    x['word'], x['lens'] = torch.tensor([data[0]['token2idx']]), torch.tensor([data[0]['seq_len']])
    
    if cfg.model_name != 'lm':
        x['head_pos'], x['tail_pos'] = torch.tensor([data[0]['head_pos']]), torch.tensor([data[0]['tail_pos']])
        if cfg.model_name == 'cnn':
            if cfg.use_pcnn:
                x['pcnn_mask'] = torch.tensor([data[0]['entities_pos']])
        if cfg.model_name == 'gcn':
            # ????????????????????? parsing tree ?????????????????????????????????
            adj = torch.empty(1,data[0]['seq_len'],data[0]['seq_len']).random_(2)
            x['adj'] = adj


    for key in x.keys():
        x[key] = x[key].to(device)

    with torch.no_grad():
        y_pred = model(x)
        y_pred = torch.softmax(y_pred, dim=-1)[0]
        prob = y_pred.max().item()
        prob_rel = list(rels.keys())[y_pred.argmax().item()]
        logger.info(f"\"{data[0]['head']}\" ??? \"{data[0]['tail']}\" ?????????????????????\"{prob_rel}\"???????????????{prob:.2f}???")


def _output_result(cfg, sentence, head, tail):
    # get predict instance
    instance = _get_predict_instance(cfg, sentence, head, tail)
    data = [instance]

    # preprocess data
    data, rels = _preprocess_data(data, cfg)

    # model
    __Model__ = {
        'cnn': models.PCNN,
        'rnn': models.BiLSTM,
        'transformer': models.Transformer,
        'gcn': models.GCN,
        'capsule': models.Capsule,
        'lm': models.LM,
    }

    # ????????? cpu ?????????
    cfg.use_gpu = False
    if cfg.use_gpu and torch.cuda.is_available():
        device = torch.device('cuda', cfg.gpu_id)
    else:
        device = torch.device('cpu')
    # logger.info(f'device: {device}')

    model = __Model__[cfg.model_name](cfg)
    # logger.info(f'model name: {cfg.model_name}')
    # logger.info(f'\n {model}')
    model.load(cfg.fp, device=device)
    model.to(device)
    model.eval()

    x = dict()
    x['word'], x['lens'] = torch.tensor([data[0]['token2idx']]), torch.tensor([data[0]['seq_len']])
    
    if cfg.model_name != 'lm':
        x['head_pos'], x['tail_pos'] = torch.tensor([data[0]['head_pos']]), torch.tensor([data[0]['tail_pos']])
        if cfg.model_name == 'cnn':
            if cfg.use_pcnn:
                x['pcnn_mask'] = torch.tensor([data[0]['entities_pos']])
        if cfg.model_name == 'gcn':
            # ????????????????????? parsing tree ?????????????????????????????????
            adj = torch.empty(1,data[0]['seq_len'],data[0]['seq_len']).random_(2)
            x['adj'] = adj


    for key in x.keys():
        x[key] = x[key].to(device)

    with torch.no_grad():
        y_pred = model(x)
        y_pred = torch.softmax(y_pred, dim=-1)[0]
        prob = y_pred.max().item()
        prob_rel = list(rels.keys())[y_pred.argmax().item()]
        logger.info(f"\"{data[0]['head']}\" ??? \"{data[0]['tail']}\" ?????????????????????\"{prob_rel}\"???????????????{prob:.2f}???")

    return data[0]['head'], data[0]['tail'], prob_rel



@hydra.main(config_path='conf/config.yaml')
def main(cfg):
    cwd = utils.get_original_cwd()
    # cwd = cwd[0:-5]
    cfg.cwd = cwd
    cfg.pos_size = 2 * cfg.pos_limit + 2
    # print(cfg.pretty())

    # load ner result
    nodes,sentence=_load_ner_result()

    header_list = ["Source", "Type", "Target"]
    data_list=[]

    with open("C:/Users/gzy88/Desktop/KnowledgeGraph/API.DAL/NLP/data/re_result.csv", mode="w", encoding="utf-8", newline="") as f:
        for i in range(0, len(nodes)):
            for j in range(i, len(nodes)):
                if j==i:
                    continue
                else:
                    head=nodes[i]
                    tail=nodes[j]
                    source,target,type=_output_result(cfg, sentence, head, tail)
                    data_list.append({"Source":source, "Type":type, "Target":target})

        writer = csv.DictWriter(f, header_list)
        writer.writeheader()
        writer.writerows(data_list)

    logger.info("?????????????????????")


if __name__ == '__main__':
    main()
