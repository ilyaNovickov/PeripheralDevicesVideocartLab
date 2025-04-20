using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace VideocartLab.View.Avalonia.Controls;

public partial class NodeList : UserControl, INodeListView
{
    public NodeList()
    {
        InitializeComponent();
    }

    private void ListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        //Только добавленные эелменты
        var selectedItems = e.AddedItems;

        if (selectedItems.Count == 0)
        {

            return;
        }


    }


    public IEnumerable? ItemsList
    {
        get => listBox.ItemsSource;
        set => listBox.ItemsSource = value;
    }
}