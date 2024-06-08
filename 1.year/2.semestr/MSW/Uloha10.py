import numpy as np
from scipy.integrate import solve_ivp
import matplotlib.pyplot as plt

# Definice modelu kapacitního růstu
def logistic_growth(t, y, r, K):
    dydt = r * y * (1 - y / K)
    return dydt

# Počáteční hodnota a parametry
y0 = [1]  # Počáteční populace
r = 0.1   # Rychlost růstu
K = 200   # Kapacita prostředí

# Časový interval
t_span = (0, 150)

# Numerické řešení
sol = solve_ivp(logistic_growth, t_span, y0, args=(r, K), t_eval=np.linspace(*t_span, 1000))

# Vizualizace výsledků
plt.figure(figsize=(10, 6))
plt.plot(sol.t, sol.y[0])
plt.xlabel('Čas')
plt.ylabel('Populace')
plt.title('Kapacitní růst populace')
plt.grid(True)
plt.show()

# Z modelovaného procesu lze vyčíst, že jakmile se počet obyvatel začne blížit ke kapacitě prostředí,
# začne se rychlost růstu populace zpomalovat a ústálí se kolem hodnoty K (Kapacita prostředí).