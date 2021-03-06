﻿using System;
using System.Collections;

[System.Serializable]
public class NeuralNetwork
{
    public NeuralLayer[] Layers;

    public int[] Topology;

    public NeuralNetwork(SerializeableNeuralNetwork loadedNetwork)
    {
        this.Topology = loadedNetwork.topology;

        Layers = new NeuralLayer[loadedNetwork.topology.Length - 1];

        for (int i = 0; i < Layers.Length; i++)
        {
            Layers[i] = new NeuralLayer(loadedNetwork.layers[i]);
        }
    }


    public NeuralNetwork(params int[] topology)
    {
        this.Topology = topology;

        Layers = new NeuralLayer[topology.Length - 1];


        for (int i = 0; i<Layers.Length; i++)
        {
            Layers[i] = new NeuralLayer(topology[i], topology[i + 1]);
        }
    }

    public double[] CalculateYValues(double[] xValues)
    {
        if (xValues.Length != Layers[0].NodeCount)
            throw new ArgumentException("Given xValues do not match network input amount.");

        double[] output = xValues;
        foreach (NeuralLayer layer in Layers)
            output = layer.CalculateYValues(output);

        return output;
        
    }

    public void FillWithRandomWeights(double rangeStart, double rangeEnd)
    {
        if (Layers != null)
        {
            foreach (NeuralLayer layer in Layers)
                layer.FillWithRandomWeights(rangeStart, rangeEnd);
        }
    }

    public NeuralNetwork GetTopologyCopy()
    {
        NeuralNetwork copy = new NeuralNetwork(this.Topology);
        for (int i = 0; i < Layers.Length; i++)
            copy.Layers[i].CurrentActivationFunction = this.Layers[i].CurrentActivationFunction;

        return copy;
    }

    public NeuralNetwork DeepCopy()
    {
        NeuralNetwork newNet = new NeuralNetwork(this.Topology);
        for (int i = 0; i < this.Layers.Length; i++)
            newNet.Layers[i] = this.Layers[i].DeepCopy();

        return newNet;
    }
}
