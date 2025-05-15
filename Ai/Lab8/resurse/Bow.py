import re
from nltk.corpus import stopwords
from nltk.tokenize import word_tokenize
import nltk
from nltk.stem import WordNetLemmatizer
from nltk.corpus import wordnet


def get_wordnet_pos(word):
    tag = nltk.pos_tag([word])[0][1][0].upper()
    tag_dict = {"J": wordnet.ADJ, "N": wordnet.NOUN, "V": wordnet.VERB, "R": wordnet.ADV}
    return tag_dict.get(tag, wordnet.NOUN)


def preprocess(dataset):
    stop_words = set(stopwords.words('english'))
    new_dataset = []

    for data in dataset:
        data = data.lower()
        data = re.sub(r'\W', ' ', data)
        lemmatizer = WordNetLemmatizer()
        words = [lemmatizer.lemmatize(word, get_wordnet_pos(word)) for word in word_tokenize(data)]
        words = [word for word in words if word not in stop_words]
        new_dataset.append(words)

    return new_dataset


def vocabulary(dataset):
    voc = {}
    word_index = 0
    for data in dataset:
        for word in data:
            if word not in voc.keys():
                voc[word] = word_index
                word_index += 1

    return voc


def bow_datasets(dataset):
    dataset = preprocess(dataset)
    voc = vocabulary(dataset)

    bow_matrix = []
    for data in dataset:
        X = [0] * len(voc)
        for word in data:
            X[voc[word]] += 1
        bow_matrix.append(X)

    return bow_matrix