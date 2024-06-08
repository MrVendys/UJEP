import numpy as np
import time

# Vytvoříme dvě náhodná pole
array1 = np.random.rand(10**7)
array2 = np.random.rand(10**7)

# Měření času pro NumPy
start_time = time.time()
result = np.dot(array1, array2)
end_time = time.time()
print(f"Čas výpočtu s NumPy: {end_time - start_time} sekundy")

# Měření času pro Python
start_time = time.time()
result_python = sum(x * y for x, y in zip(array1, array2))
end_time = time.time()
print(f"Čas výpočtu bez NumPy: {end_time - start_time} sekundy")

