﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IBL;
using BL;
using BL;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {

        IBLInterface bl;
        internal ObservableCollection<Drone> drones;
        internal ObservableCollection<StationToList> stations;
        internal ObservableCollection<PackageToList> packages;
        internal ObservableCollection<CustomerToList> customers;
        public ListWindow(IBLInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
            drones = new(bl.GetDroneList().Select(d => new Drone(d)));
            DroneListView.DataContext = drones;
            stations = new(bl.GetStationList());
            StationListView.DataContext = stations;
            packages = new(bl.GetPackageList());
            PackageListView.DataContext = packages;
            customers = new(bl.GetCustomerList());
            CustomerListView.DataContext = customers;
            DroneStatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            DroneWeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));           
        }

        private void DroneStatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void DroneWeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            if (DroneStatusSelector.SelectedItem == null && DroneWeightSelector.SelectedItem == null)
                DroneListView.ItemsSource = drones;
            else if (DroneStatusSelector.SelectedItem == null)
                DroneListView.ItemsSource = drones.Where(d => d.weightCategory == GetWeight());
            else if (DroneWeightSelector.SelectedItem == null)
                DroneListView.ItemsSource = drones.Where(d => d.status == GetStatus());
            else
                DroneListView.ItemsSource = drones.Where(d => d.status == GetStatus() && d.weightCategory == GetWeight());
        }

        private DroneStatuses GetStatus()
        {
            return (DroneStatuses)DroneStatusSelector.SelectedItem;            
        }

        private WeightCategories GetWeight()
        {
            return (WeightCategories)DroneWeightSelector.SelectedItem;            
        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            new DroneWindow(bl, drones).Show();
        }

        private void DroneActionWindow(object sender, MouseButtonEventArgs e)
        {
            Drone drone = (Drone)DroneListView.SelectedItem;
            new DroneWindow(bl, drone, drones).Show();
        }

       

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            DroneListView.ItemsSource = drones;
            DroneStatusSelector.SelectedIndex = DroneWeightSelector.SelectedIndex = -1;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
