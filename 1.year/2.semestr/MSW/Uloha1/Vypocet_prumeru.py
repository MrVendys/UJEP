import numpy as np
import random
import time

# Nastavíme délku seznamu
list_length = 1000000

# Generujeme náhodný seznam čísel od 0 do 999999
python_list = [random.randint(0, 999999) for i in range(list_length)]
numpy_array = np.array(python_list)

# Měření času pro Python
start_time = time.time()
average_python = sum(python_list) / len(python_list)
end_time = time.time()
print(f"Čas výpočtu bez NumPy: {end_time - start_time} sekundy")

# Měření času pro NumPy
start_time = time.time()
average_numpy = np.mean(numpy_array)
end_time = time.time()
numpy_time = end_time - start_time
print(f"Čas výpočtu s NumPy: {end_time - start_time} sekundy")