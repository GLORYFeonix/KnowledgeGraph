from deepke.name_entity_re.standard import *
import hydra
from hydra import utils
import csv

@hydra.main(config_path="conf", config_name='config')
def main(cfg):
    model = InferNer(utils.get_original_cwd()+'/'+"checkpoints/")
    # text = cfg.text

    text=""
    with open("C:/Users/gzy88/Desktop/KnowledgeGraph/API.DAL/NLP/data/single.txt",encoding="utf-8") as f:
        text=f.readline().strip()

    header_list = ["Node", "Type"]
    data_list=[]

    result = model.predict(text)

    with open("C:/Users/gzy88/Desktop/KnowledgeGraph/API.DAL/NLP/data/ner_result.csv", mode="w", encoding="utf-8", newline="") as f:
        for k,v in result.items():
            if v:
                for i in v:
                    data_list.append({"Node":i,"Type":k})
        writer = csv.DictWriter(f, header_list)
        writer.writeheader()
        writer.writerows(data_list)
   
    
if __name__ == "__main__":
    main()
