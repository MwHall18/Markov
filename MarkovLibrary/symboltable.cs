using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace markov;

public class ListSymbolTable<K, V> : IEnumerable<K> where K : IComparable
{

    private class Node<K, V>
    {
        public K key;
        public V value;
        public Node<K, V> next;

        public Node(K key, V value = default)
        {
            this.key = key;
            this.value = value;
            next = null;
        }
    }

    private Node<K, V> head;
    private int count;

    public int Count
    {
        get { return count; }
    }

    public ListSymbolTable()
    {
        head = null;
        count = 0;
    }

    public V this[K key]
    {
        get
        {
            Node<K, V> node = FindNode(key);
            if (node == null)
            {
                throw new KeyNotFoundException("");
            }
            else
            {
                return node.value;
            }
        }
        set
        {
            Node<K, V> node = FindNode(key);
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

    public void Add(K key, V value)
    {
        if (FindNode(key) != null)
        {
            throw new ArgumentException("");
        }
        else
        {
            Node<K, V> node = new Node<K, V>(key, value);
            node.next = head;
            head = node;
            count++;
        }
    }

    public bool ContainsKey(K key)
    {
        Node<K, V> curr = head;
        while (curr != null)
        {
            if (curr.key.Equals(key))
            {
                return true;
            }
            curr = curr.next;
        }
        return false;
    }

    private Node<K, V> FindNode(K key)
    {
        Node<K, V> curr = head;
        while (curr != null)
        {
            if (curr.key.Equals(key))
            {
                return curr;
            }
            curr = curr.next;
        }
        return null;
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<K> GetEnumerator()
    {
        Node<K, V> curr = head;
        while (curr != null)
        {
            yield return curr.key;
            curr = curr.next;
        }
    }

}