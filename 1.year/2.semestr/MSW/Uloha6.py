import os
import hashlib
import random

# Získání sériového čísla pevného disku na Windows
def get_disk_serial_number():
    # Použijeme nástroj wmic pro získání sériového čísla pevného disku
    result = os.popen("wmic diskdrive get SerialNumber").read()
    serial_number = result.strip().split("\n")[1].strip()
    return serial_number

disk_serial = get_disk_serial_number()

# Použijeme sériové číslo pevného disku k vytvoření hashu
disk_hash = hashlib.sha256(disk_serial.encode()).digest()
    
# Použijeme hash jako semínko pro generování náhodných čísel
random.seed(int.from_bytes(disk_hash, 'big'))
    
# Generování náhodných čísel
random_numbers = [random.randint(1, 100) for _ in range(10)]
    
print("Náhodná čísla generovaná na základě sériového čísla pevného disku:", random_numbers)
