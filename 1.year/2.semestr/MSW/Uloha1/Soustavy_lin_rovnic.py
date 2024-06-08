import numpy as np
import random
import time

# Nastavíme počet rovnic a neznámých
n = 100

# Generujeme náhodnou matici soustavy rovnic A a vektor pravých stran b
A = np.random.rand(n, n)
b = np.random.rand(n)

# Měření času pro Python
start_time = time.time()

# Řešení soustavy rovnic pomocí standardního Pythonu (Gaussova eliminační metoda)
def gaussian_elimination(A, b):
    n = len(b)
    x = np.zeros(n)

    for i in range(n):
        for j in range(i+1, n):
            ratio = A[j, i] / A[i, i]
            A[j, i:] -= ratio * A[i, i:]
            b[j] -= ratio * b[i]

    for i in range(n - 1, -1, -1):
        x[i] = (b[i] - np.dot(A[i, i+1:], x[i+1:])) / A[i, i]

    return x

solution_python = gaussian_elimination(A, b)

end_time = time.time()
print(f"Čas výpočtu bez NumPy: {end_time - start_time} sekundy")

# Měření času pro NumPy
start_time = time.time()
solution_numpy = np.linalg.solve(A, b)
end_time = time.time()
print(f"Čas výpočtu s NumPy: {end_time - start_time} sekundy")
