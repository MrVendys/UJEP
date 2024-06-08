import numpy as np

# Funkce, kterou budeme derivovat
def f(x):
    return np.exp(x)

def adaptive_derivative(func, x, h=1e-5, tolerance=1e-6):
    # Počáteční krok
    step = h
    # Inicializace derivace
    derivative = (func(x + step) - func(x - step)) / (2 * step)
    
    while True:
        step /= 2  # Polovina kroku
        new_derivative = (func(x + step) - func(x - step)) / (2 * step)
        
        # Rozdíl mezi novou a starou derivací
        diff = abs(new_derivative - derivative)
        
        if diff < tolerance:
            break  # Přesnost dosažena
        
        derivative = new_derivative
    
    return derivative

# Bod, ve kterém chceme derivaci
x0 = 1.0

# Analytická derivace exponenciální funkce
analytical_derivative = np.exp(x0)

# Pevný krok pro numerickou derivaci
static_step = 1e-4
numeric_derivative_static = (f(x0 + static_step) - f(x0 - static_step)) / (2 * static_step)

# Numerická derivace s adaptabilním krokem
numeric_derivative_adaptive = adaptive_derivative(f, x0)

print("Analytická derivace:", analytical_derivative)
print("Numerická derivace (pevný krok):", numeric_derivative_static)
print("Numerická derivace (adaptabilní krok):", numeric_derivative_adaptive)

