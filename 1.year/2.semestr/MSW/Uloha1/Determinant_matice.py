import numpy as np
import random
import time

# Nastavíme rozměry matice
matrix_size = 10

# Generujeme náhodnou matici
matrix = np.random.rand(matrix_size, matrix_size)

# Měření času pro Python
start_time = time.time()

# Funkce pro výpočet determinantu matice v čistém Pythonu
# Trvá v průměru 20 sekund
def calculate_determinant(matrix):
    size = len(matrix)
    if size == 1:
        return matrix[0, 0]
    elif size == 2:
        return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]
    else:
        determinant = 0
        for i in range(size):
            submatrix = np.delete(np.delete(matrix, i, axis=0), 0, axis=1)
            determinant += matrix[0, i] * calculate_determinant(submatrix)
        return determinant

determinant_python = calculate_determinant(matrix)

end_time = time.time()
print(f"Čas výpočtu bez NumPy: {end_time - start_time} sekundy")

# Měření času pro NumPy
start_time = time.time()
determinant_numpy = np.linalg.det(matrix)
end_time = time.time()
print(f"Čas výpočtu s NumPy: {end_time - start_time} sekundy")

