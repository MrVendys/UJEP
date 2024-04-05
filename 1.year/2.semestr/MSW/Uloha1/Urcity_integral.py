import time
from scipy.integrate import quad

# Definujeme funkci, kterou budeme integrovat
def f(x):
    return x**2

# Definujeme interval, ve kterém budeme integrovat
a = 0
b = 1

# Měření času pro Python
start_time = time.time()

# Aproximace určitého integrálu v Pythonu
n = 1000000  # Počet kroků pro aproximaci
integral_python = 0
step = (b - a) / n

for i in range(n):
    x = a + i * step
    integral_python += f(x) * step

end_time = time.time()
print(f"Čas výpočtu bez Scipy: {end_time - start_time} sekundy")

# Měření času pro SciPy
start_time = time.time()
integral_scipy = quad(f, a, b)
end_time = time.time()
print(f"Čas výpočtu s Scipy: {end_time - start_time} sekundy")
