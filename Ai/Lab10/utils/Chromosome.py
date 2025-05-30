from random import randint, uniform


class Chromosome:
    def __init__(self, problParam=None):
        self.__problParam = problParam
        self.repres = [generateNewLabel(1, problParam['noNodes']) for _ in range(problParam['noNodes'])]
        self.fitness = 0.0

    def crossover(self, c):
        noNodes = len(self.repres)
        newrepres = []
        for i in range(noNodes):
            prob = uniform(0, 1)
            if prob > 0.5:
                newrepres.append(c.repres[i])
            else:
                newrepres.append(self.repres[i])
        offspring = Chromosome(self.__problParam)
        offspring.repres = newrepres
        return offspring

    def mutation(self):
        pos = randint(0, len(self.repres) - 1)
        self.repres[pos] = generateNewLabel(1, self.__problParam['noNodes'])

    def __eq__(self, c):
        return self.repres == c.repres and self.fitness == c.fitness


def generateNewLabel(lim1, lim2):
    return randint(lim1, lim2)
