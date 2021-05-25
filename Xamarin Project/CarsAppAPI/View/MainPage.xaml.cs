﻿using CarsAppAPI.Models;
using CarsAppAPI.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarsAppAPI
{
    public partial class MainPage : MasterDetailPage
    {
        public List<MenuPagina> MiMenu { get; set; }
        public MainPage()
        {
            InitializeComponent();
            MiMenu = new List<MenuPagina>();

            MenuPagina pag1 = new MenuPagina() { Titulo = "Select", Pagina = typeof(Select) };
            MiMenu.Add(pag1);

            MenuPagina pag2 = new MenuPagina() { Titulo = "Insert", Pagina = typeof(Insert) };
            MiMenu.Add(pag2);

            MenuPagina pag3 = new MenuPagina() { Titulo = "Update", Pagina = typeof(Update) };
            MiMenu.Add(pag3);

            MenuPagina pag4 = new MenuPagina() { Titulo = "Delete", Pagina = typeof(Delete) };
            MiMenu.Add(pag4);

            MenuPagina pag5 = new MenuPagina() { Titulo = "QRCode", Pagina = typeof(QRCode) };
            MiMenu.Add(pag5);

            MenuPagina pag6 = new MenuPagina() { Titulo = "QRCodeScan", Pagina = typeof(QRScan) };
            MiMenu.Add(pag6);

            MenuPagina pag7 = new MenuPagina() { Titulo = "Login", Pagina = typeof(login) };
            MiMenu.Add(pag7);
            this.lsvmenu.ItemsSource = MiMenu;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Detail)));

            this.lsvmenu.ItemSelected += Lsvmenu_ItemSelected;

        }
        private void Lsvmenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)

        {
            MenuPagina pagina = e.SelectedItem as MenuPagina;

            Detail = new NavigationPage((Page)Activator.CreateInstance(pagina.Pagina));

            IsPresented = false;

        }
    }
}