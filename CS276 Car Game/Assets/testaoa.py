from typing import List, Tuple, Dict

def two_sum_hash_table(arr: List[int]) -> Tuple[int, int]:
    """
    Count pairs that sum to exactly 0 using hash table
    
    Args:
        arr: List of integers
        
    Returns:
        Tuple of (count of zero-sum pairs, number of lookups made)
        
    Time Complexity: O(n)
    Space Complexity: O(n)
    """
    frequency: Dict[int, int] = {}
    lookups = 0
    
    # First pass: count frequencies
    for num in arr:
        frequency[num] = frequency.get(num, 0) + 1
        lookups += 1
    
    count = 0
    processed: set = set()
    
    # Second pass: count pairs
    for num in frequency:
        lookups += 1
        
        if num in processed:
            continue
            
        if num == 0:
            # Special case: pairs of zeros
            # Choose 2 from frequency[0] zeros: C(n,2) = n*(n-1)/2
            zeros = frequency[0]
            count += (zeros * (zeros - 1)) // 2
        elif -num in frequency:
            # Regular case: positive-negative pairs
            # Each positive can pair with each negative
            count += frequency[num] * frequency[-num]
            processed.add(num)
            processed.add(-num)
    
    return count, lookups

# Test the hash table solution
test_arrays = [
    [1, -1, 2, -2, 3],
    [0, 0, 0],
    [5, -5, 10, -10, 5, -5],
    [1, 2, 3, 4, 5]
]

print("Hash Table Approach Results:")
print("-" * 50)
for arr in test_arrays:
    result, lookups = two_sum_hash_table(arr)
    print(f"Array: {arr}")
    print(f"  Zero-sum pairs: {result}")
    print(f"  Hash operations: {lookups}")
    print()