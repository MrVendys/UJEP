import numpy as np
import random
import time

# Nastavíme rozměry matic
matrix_size = 100

# Generujeme náhodné matice
matrix_A = np.random.rand(matrix_size, matrix_size)
matrix_B = np.random.rand(matrix_size, matrix_size)

# Měření času pro Python
start_time = time.time()
result_python = [[0 for _ in range(matrix_size)] for _ in range(matrix_size)]
for i in range(matrix_size):
    for j in range(matrix_size):
        for k in range(matrix_size):
            result_python[i][j] += matrix_A[i][k] * matrix_B[k][j]
end_time = time.time()
print(f"Čas výpočtu bez NumPy: {(end_time - start_time)} sekundy")

# Měření času pro NumPy
start_time = time.time()
result_numpy = np.dot(matrix_A, matrix_B)
end_time = time.time()
print(f"Čas výpočtu pro NumPy: {(end_time - start_time)} sekundy")





