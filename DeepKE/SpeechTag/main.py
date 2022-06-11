import os

import jieba
import jieba.posseg as pseg

import BIOtag

inputfile = "data/input/钛合金表面技术的进展_刘凤岭.txt"
outputfile = "data/output/test.txt"

with open(inputfile, 'r', encoding='utf-8') as f:
    content = f.read().strip()
    jieba.load_userdict("data/Ti_jieba_dict.txt")
    words = pseg.cut(content, use_paddle=True)

# flags = ["n", "nr", "nz", "PER", "f", "ns", "LOC", "s", "nt", "an", "ORG", "t", "nw", "vn", "TIME"]
flags = "nz"
keywords = []

with open("data/word_dict.txt", 'w', encoding="utf-8") as o:
    for word, flag in words:
        if flag == flags:
            if word not in keywords:
                keywords.append(word)
                o.write(word + " " + flag + '\n')
            else:
                continue
    o.write("占位词 NONE")

BIOtag.tag(inputfile, outputfile)

os.remove("data/word_dict.txt")
