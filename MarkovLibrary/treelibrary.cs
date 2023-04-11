using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace markov;

internal class Node<K, V>
{
    public K key;
    public V value;
    public Node<K, V>? L;
    public Node<K, V>? R;
    public int count;

    public Node(K key, V value = default!)
    {
        this.key = key;
        this.value = value;
        L = null;
        R = null;
        count = 1;

    }

    public int Count
    {
        get
        {
            return this.count;

        }
    }

    public override string ToString()
    {
        string s = "";
        if (this.L == null)
        {
            s += "NULL";
        }
        else
        {
            s += this.L.key;
        }

        s += $" <- {this.key} -> ";

        if (this.R == null)
        {
            s += "NULL";
        }
        else
        {
            s += this.R.key;
        }

        return s;
    }
}

public class TreeSymbolTable<K, V> : System.Collections.Generic.IEnumerable<K> where K : IComparable<K>
{
    private Node<K, V>? root;

    public TreeSymbolTable()
    {
        root = null;
    }

    public V this[K key]
    {
        get
        {
            Node<K, V>? node = GetNode(root, key);
            if (node == null)
            {
                throw new KeyNotFoundException($"Key '{key}' does not exist in the symbol table");
            }
            else
            {
                return node.value;
            }
        }

        set
        {
            Node<K, V>? node = GetNode(root, key);
            if (node == null)
            {
                Add(key, value);
            }
            else
            {
                node.value = value;
            }
        }
    }

    public K Successor()
    {
        return Min(root.R);
    }

    private K Successor(K key)
    {
        Node<K, V>? node = GetNode(root, key);
        if (node.R == null)
        {
            return Min(root.R);
        }
        else
        {
            throw new InvalidOperationException($"Thers's no successor to {key}");
        }
        return Min(root.R);
    }

    public K Min()
    {
        return Min(root);
    }

    private K Min(Node<K, V>? subroot)
    {
        ArgumentNullException.ThrowIfNull(subroot);
        if (subroot == null)
        {
            return Min(subroot.L);
        }
        else
        {
            return subroot.key;
        }
    }

    public K Max()
    {
        return Max(root);
    }

    private K Max(Node<K, V>? subroot)
    {
        ArgumentNullException.ThrowIfNull(subroot);
        if (subroot == null)
        {
            return Max(subroot.R);
        }
        else
        {
            return subroot.key;
        }
    }

    public bool ContainsKey(K key)
    {
        Node<K, V>? node = GetNode(root, key);
        if (node == null)
        {
            return false;
            //throw new KeyNotFoundException($"Key '{key}' does not exist in the symbol table");
        }
        else
        {
            return true;
        }
    }

    private Node<K, V> GetNode(Node<K, V> subroot, K key)
    {
        if (subroot != null)
        {
            if (key.CompareTo(subroot.key) == -1)
            {
                return GetNode(subroot.L, key);
            }
            else if (key.CompareTo(subroot.key) == 1)
            {
                return GetNode(subroot.R, key);
            }
            else if (key.CompareTo(subroot.key) == 0)
            {
                return subroot;
            }
        }
        return null;
    }

    public void Add(K key, V value)
    {
        root = Add(key, value, root);
    }

    private Node<K, V> Add(K key, V value, Node<K, V>? subroot)
    {
        if (subroot == null)
        {
            return new Node<K, V>(key, value);
        }

        ArgumentNullException.ThrowIfNull(key);
        /*
        if (key == null)
        {
            throw new ArgumentNullException("Key cannot be null"); // similar to ArgumentNullException
        }
        */
        if (key.CompareTo(subroot.key) == -1)
        {
            subroot.L = Add(key, value, subroot.L);
            subroot.count++;
            return subroot;
        }
        else if (key.CompareTo(subroot.key) == 1)
        {
            subroot.R = Add(key, value, subroot.R);
            subroot.count++;
            return subroot;
        }
        else
        {
            throw new ArgumentException($"Key '{key}' already exists");
        }
    }

    public void RemoveAt(K key)
    {
        root = remove(key, root);
    }

    private Node<K, V> remove(K key, Node<K, V> subroot)
    {
        if (subroot == null)
        {
            return subroot;
        }
        int cmp = key.CompareTo(subroot.key);
        if (cmp < 0)
        {
            subroot.L = remove(key, subroot.L);
        }
        else if (cmp > 0)
        {
            subroot.R = remove(key, subroot.R);
        }
        else
        {
            if (subroot.R == null)
            {
                subroot.count--;
                return subroot.L;
            }
            else if (subroot.L == null)
            {
                subroot.count--;
                return subroot.R;
            }
            subroot.key = Min(subroot.R);
            subroot.R = remove(subroot.key, subroot.R);
        }
        return subroot;
    }

    public void PrintInOrder()
    {
        PrintInOrder(root);
    }

    private void PrintInOrder(Node<K, V>? subroot)
    {
        if (subroot == null)
        {
            return;
        }

        PrintInOrder(subroot.L);
        Console.WriteLine(subroot.key);
        PrintInOrder(subroot.R);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public System.Collections.Generic.IEnumerator<K> GetEnumerator()
    {
        foreach (K key in GetEnumerator(root)) yield return key;
    }

    private System.Collections.Generic.IEnumerable<K> GetEnumerator(Node<K, V>? subroot)
    {
        if (subroot != null)
        {
            foreach (K key in GetEnumerator(subroot.L)) yield return key;
            yield return subroot.key;
            foreach (K key in GetEnumerator(subroot.R)) yield return key;
        }
    }

}