﻿using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace KantorClient.Application.ControlExtenders
{
    public class DataGridExtended : DataGrid
    {
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(IList), typeof(DataGridExtended), new PropertyMetadata(default(IList)));

        public new IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { throw new Exception("This property is read-only. To bind to it you must use 'Mode=OneWayToSource'."); }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            SetValue(SelectedItemsProperty, base.SelectedItems);
        }
    }
}
