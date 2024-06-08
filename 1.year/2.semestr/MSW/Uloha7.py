import random

# Máme nepravidelný čtyřúhelník se vrcholy (0, 0), (1, 1), (2, 2) a (1, 3). 
#3   .
#2     .
#1   .
#0 .
#  0 1 2
# Definice funkce pro určení, zda bod leží uvnitř nepravidelného čtyřúhelníku
def is_inside(x, y):
    return 0 <= x <= 2 and 0 <= y <= 3 and y - x <= 2

# Počet náhodných bodů, které vygenerujeme
num_points = 10000

# Počet bodů uvnitř čtyřúhelníku
points_inside = 0

# Generování náhodných bodů a určení, zda leží uvnitř čtyřúhelníku
for _ in range(num_points):
    x = random.uniform(0, 2)  # x v rozmezí [0, 2]
    y = random.uniform(0, 3)  # y v rozmezí [0, 3]
    if is_inside(x, y):
        points_inside += 1

# Poměr bodů uvnitř čtyřúhelníku k celkovému počtu bodů je přibližně roven poměru plochy čtyřúhelníku k ploše obdélníka
estimated_area = points_inside / num_points * 2 * 3

print("Odhad plochy nepravidelného čtyřúhelníku:", estimated_area)
