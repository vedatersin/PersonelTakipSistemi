
import sys

def check_brace_balance(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        lines = f.readlines()
        
    balance = 0
    for i, line in enumerate(lines):
        line_num = i + 1
        for char in line:
            if char == '{':
                balance += 1
            elif char == '}':
                balance -= 1
        
        if balance < 0:
            print(f"Error: Balance went negative at line {line_num}")
            return
        
        # In a file with a namespace and a class, balance 0 means closed everything.
        # Namespace starts at line 20 (approx) with { -> balance 1
        # Class starts at line 24 (approx) with { -> balance 2
        # If balance drops to 1, the class is CLOSED.
        # If balance drops to 0, the namespace is CLOSED.
        
        if line_num > 30 and balance == 1:
            print(f"Class CLOSED at line {line_num}: {line.strip()}")
        if line_num > 30 and balance == 0:
            print(f"Namespace CLOSED at line {line_num}: {line.strip()}")
            return

if __name__ == "__main__":
    check_brace_balance(sys.argv[1])
