using System;
using System.Collections.Generic;
using UnityEngine;

namespace Statistics
{
    public class DecisionTree : MonoBehaviour
{
    private readonly Node _rootNode;

    public DecisionTree(Node rootNode)
    {
        _rootNode = rootNode;
    }

    // Getting data from the file
    public void Learn(List<string[]> data)
    {
        List<string[]> subset = new List<string[]>();
        List<string> feelings = new List<string>(subset[0]);
        
        BuildTree(subset, feelings, _rootNode);
    }

    void BuildTree(List<string[]> data, List<string> feelings, Node node)
    {
        //Check for stopping criteria (e.g., all instances belong to the same class)
        if (data.TrueForAll(row => row[^1] == data[0][data[0].Length - 1]))
        {
            node.Game = data[0][data[0].Length - 1];
            return;
        }

        // Select the feeling to split on based on our prior knowledge
        string selectedFeeling = "";
        
        // Iterate through the possible values of the selected feeling
        foreach (var value in GetDistinctValues(data, feelings.IndexOf(selectedFeeling)))
        {
            Node child = new Node();
            child.Feeling = selectedFeeling;

            // Filter the data based on the selected feeling value
            List<string[]> subset = data.FindAll(row => row[feelings.IndexOf(selectedFeeling)] == value);

            // Handle the case when no instances match the selected feelings value
            if (subset.Count == 0) child.Game = GetMostCommonClass(data);
            else
            {
                // Remove the selected feeling from the list of available feelings
                List<string> remainingFeelings = new List<string>(feelings);
                remainingFeelings.Remove(selectedFeeling);
                
                // Recursively build the decision tree for the subset
                BuildTree(subset, remainingFeelings, child);
            }
            
            node.Children.Add(value, child);
        }
        
        string GetMostCommonClass(List<string[]> data)
        {
            Dictionary<string, int> classCounts = new Dictionary<string, int>();

            foreach (var row in data)
            {
                string targetClass = row[^1];

                if (classCounts.ContainsKey(targetClass))
                    classCounts[targetClass]++;
                else
                    classCounts[targetClass] = 1;
            }

            int maxCount = 0;
            string result = "";

            foreach (var entry in classCounts)
            {
                if (entry.Value > maxCount)
                {
                    maxCount = entry.Value;
                    result = entry.Key;
                }
            }

            return result;
        }
    }

    // Getting all the data for attribute index
    List<string> GetDistinctValues(List<string[]> data, int columnIndex)
    {
        HashSet<string> distinctValues = new HashSet<string>();

        foreach (var row in data)
            distinctValues.Add(row[columnIndex]);
        
        return new List<string>(distinctValues);
    }

    // Function to get the game based on feelings 
    public string Classify(string[] instance) => TraverseTree(instance, _rootNode);

    string TraverseTree(string[] instance, Node node)
    {
        if (node.Children.Count == 0)
            return node.Game;

        string featureValue = instance[Array.IndexOf(instance, node.Feeling)];

        if (node.Children.ContainsKey(featureValue))
            return TraverseTree(instance, node.Children[featureValue]);

        return "";
    }
}

public class Node
{
    public string Feeling { get; set; }
    public Dictionary<string, Node> Children { get; set; }
    public string Game { get; set; }

    public Node()
    {
        Children = new Dictionary<string, Node>();
        Game = "";
    }
}
}
