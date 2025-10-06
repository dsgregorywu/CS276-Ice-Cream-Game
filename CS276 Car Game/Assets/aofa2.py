# The ThreeSum problem asks: Given an array of n distinct integers and a target sum value, 
# determine if there exist three elements a, b, c in the array such that a + b + c equals the target sum.

# This is an extension of the TwoSum problem we analyzed previously. 
# While TwoSum can be solved efficiently, the naive approach to ThreeSum has significantly different performance characteristics that we'll explore in detail.

# Example:
# Input: A = [3, 7, 1, 2, 8, 4, 5], target = 13

# Output: true (because 3 + 2 + 8 = 13)


# Input: A = [1, 2, 3, 4], target = 15

# Output: false (no three elements sum to 15)

#Part 1
import random
import time
import matplotlib.pyplot as plt

def bruteforce(input, target):
    if len(input) < 3:
        return "False: Array too small."
    n = len(input)
    counter = 0
    for i in range(n):
        for j in range(n):
            for k in range(n):
                if i != j and j != k and i != k:
                    counter += 1
                    if input[i] + input[j] + input[k] == target:
                        printstr = (f"True: {input[i]} + {input[j]} + {input[k]}: {counter} Iterations")
                        return printstr
    failstr = (f"False: {counter} Iterations")
    return failstr

#Part 2



#Part 3

def run_performance_experiments():
    import matplotlib.pyplot as plt
    sizes = [50, 100, 200, 400, 800]
    trials = 10
    results = []

    def empiricaltest(arr, target):
        import time
        start = time.perf_counter()
        result = bruteforce(arr, target)
        end = time.perf_counter()
        ops = int(result.split(':')[-1].split()[0])
        runtime = end - start
        return runtime, ops

    for size in sizes:
        runtimes = []
        op_counts = []
        # With solution
        for _ in range(trials // 2):
            arr = random.sample(range(-10000, 10000), size)
            idxs = random.sample(range(size), 3)
            target = arr[idxs[0]] + arr[idxs[1]] + arr[idxs[2]]
            runtime, ops = empiricaltest(arr, target)
            runtimes.append(runtime)
            op_counts.append(ops)
        # Without solution
        for _ in range(trials // 2):
            arr = random.sample(range(-10000, 10000), size)
            target = 30001  # Out of possible sum range
            runtime, ops = empiricaltest(arr, target)
            runtimes.append(runtime)
            op_counts.append(ops)
        avg_runtime = sum(runtimes) / len(runtimes)
        avg_ops = sum(op_counts) / len(op_counts)
        results.append((size, avg_runtime, avg_ops))

    # Table
    print("\nArray Size | Avg Runtime (s) | Avg Operations Count")
    for size, runtime, ops in results:
        print(f"{size:10} | {runtime:.4f}        | {int(ops)}")
    sizes_list = [r[0] for r in results]
    runtimes_list = [r[1] for r in results]
    ops_list = [r[2] for r in results]
    theoretical_ops = [s*(s-1)*(s-2)/6 for s in sizes_list]

    plt.figure(figsize=(12,5))
    plt.subplot(1,2,1)
    plt.plot(sizes_list, runtimes_list, marker='o', label='Measured Runtime')
    plt.plot(sizes_list, [runtimes_list[0]*(s/sizes_list[0])**3 for s in sizes_list], '--', label='O(n³) Curve')
    plt.xlabel('Array Size')
    plt.ylabel('Runtime (s)')
    plt.legend()

    plt.subplot(1,2,2)
    plt.plot(sizes_list, ops_list, marker='o', label='Measured Operations')
    plt.plot(sizes_list, theoretical_ops, '--', label='O(n³) Operations')
    plt.xlabel('Array Size')
    plt.ylabel('Operations Count')
    plt.legend()
    plt.tight_layout()
    plt.show()

    # Growth Rate Calculation
    print("\nGrowth Rate Calculation:")
    for i in range(1, len(results)):
        expected_ratio = 8
        measured_ratio = runtimes_list[i] / runtimes_list[i-1]
        print(f"{sizes_list[i-1]} → {sizes_list[i]}: Expected {expected_ratio}x, Measured {measured_ratio:.2f}x")






if __name__ == "__main__":
    run_performance_experiments()
    input = [3, 7, 1, 2, 8, 4, 5]
    target = 13
    print(bruteforce(input, target))  # Should be true

    input = [1, 2, 3, 4]
    target = 15
    print(bruteforce(input, target))  # Should be false

    input = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
    target = 10
    print(bruteforce(input, target))  # Should be true

    input = [10, 20, 30, 40, 50]
    target = 100
    print(bruteforce(input, target))  # Should be true

    input = [5]
    target = 15
    print(bruteforce(input, target))  # Should be false