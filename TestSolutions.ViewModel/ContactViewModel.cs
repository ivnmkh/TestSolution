using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TestSolution.Data;

namespace TestSolutions.ViewModel
{
    public class ContactViewModel : DependencyObject
    {
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand WinLoad { get; set; }
        public Contacts editBd = new Contacts();
        public static List<Contacts> All = new List<Contacts>();

        public void reloadItems()
        {
       
            Items = null;
            Items = All;
        }
        public List<Contacts> Items
        {
            get { return (List<Contacts>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

             public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<Contacts>), typeof(ContactViewModel), new PropertyMetadata(null));

        public Contacts backItemsDG
        {
            get { return (Contacts)GetValue(backItemsDGProperty); }
            set { SetValue(backItemsDGProperty, value); }
        }

        public static readonly DependencyProperty backItemsDGProperty =
            DependencyProperty.Register("backItemsDG", typeof(Contacts), typeof(ContactViewModel), new PropertyMetadata(null, SetItemsToTB));



        public string search
        {
            get { return (string)GetValue(searchProperty); }
            set { SetValue(searchProperty, value); }
        }

        public static readonly DependencyProperty searchProperty =
            DependencyProperty.Register("search", typeof(string), typeof(ContactViewModel), new PropertyMetadata(null, GetSearch));

        private static void GetSearch(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ContactViewModel;
            current.Items = null;
            All =SqliteDataAccess.SearchContacts(current.search);
            current.Items = All;
        }

        private  void loadFromTB()
        {
                editBd.Id = TB_id;
                editBd.Name = TB_name;
                editBd.Surname = TB_surname;
                editBd.PhoneNumber = TB_phone;
                editBd.Email = TB_email;
        }
        private static void SetItemsToTB(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var curr = d as ContactViewModel;
            if (curr.backItemsDG != null) {
                curr.TB_id = curr.backItemsDG.Id;
                curr.TB_name = curr.backItemsDG.Name;
                curr.TB_surname = curr.backItemsDG.Surname;
                curr.TB_phone = curr.backItemsDG.PhoneNumber;
                curr.TB_email = curr.backItemsDG.Email;
            }
                    

        }

        public ContactViewModel()
        {
            DeleteCommand = new RelayCommand(o => DeleteContactButton());
            EditCommand = new RelayCommand(o => EditContactButton());
            AddCommand = new RelayCommand(o => AddContactButton());
            WinLoad = new RelayCommand(o => WinLoadButton());

           
        }
       
        private void DeleteContactButton()
        {
            if ((TB_name != null & !string.IsNullOrWhiteSpace(TB_name)) & (TB_surname != null & !string.IsNullOrWhiteSpace(TB_surname)) & (TB_phone != null & !string.IsNullOrWhiteSpace(TB_phone)) & (TB_email != null & !string.IsNullOrWhiteSpace(TB_email)))
            {
              
                 reloadItems();
                loadFromTB();
                SqliteDataAccess.DeleteContacts(editBd);
                WinLoadButton();
            }
        }
        private void zero()
        {
            TB_id="";
            TB_name = "";
            TB_surname = "";
            TB_phone = "";
            TB_email = "";
        }
        private void EditContactButton()
        {

            loadFromTB();
            All.Where(w => w.Id == editBd.Id).ToList().ForEach(s => s.Id= editBd.Id);
            All.Where(w => w.Id == editBd.Id).ToList().ForEach(s => s.Name = editBd.Name);
            All.Where(w => w.Id == editBd.Id).ToList().ForEach(s => s.Surname = editBd.Surname);
            All.Where(w => w.Id == editBd.Id).ToList().ForEach(s => s.PhoneNumber = editBd.PhoneNumber);
            All.Where(w => w.Id == editBd.Id).ToList().ForEach(s => s.Email = editBd.Email);
            
            SqliteDataAccess.EditContacts(editBd);
            
            reloadItems();
            MessageBox.Show("Запись изменена!");

        }
        private void AddContactButton()
        {
            if ((TB_name != null & !string.IsNullOrWhiteSpace(TB_name)) & (TB_surname != null & !string.IsNullOrWhiteSpace(TB_surname)) & (TB_phone != null & !string.IsNullOrWhiteSpace(TB_phone)) & (TB_email != null & !string.IsNullOrWhiteSpace(TB_email)))
            {

                loadFromTB();
                SqliteDataAccess.addContacts(editBd);
                All.Add(editBd);
                WinLoadButton();


            }
        }
        private void WinLoadButton()
        {
            Items = null;
            All = SqliteDataAccess.LoadContact();
            Items = All;
        }

        public string TB_id
        {
            get { return (string)GetValue(TB_idProperty); }
            set { SetValue(TB_idProperty, value); }
        }


        public static readonly DependencyProperty TB_idProperty =
            DependencyProperty.Register("TB_id", typeof(string), typeof(ContactViewModel), new PropertyMetadata(""));



        public string TB_name
        {
            get { return (string)GetValue(TB_nameProperty); }
            set { SetValue(TB_nameProperty, value); }
        }


        public static readonly DependencyProperty TB_nameProperty =
            DependencyProperty.Register("TB_name", typeof(string), typeof(ContactViewModel), new PropertyMetadata(""));


        public string TB_surname
        {
            get { return (string)GetValue(TB_surnameProperty); }
            set { SetValue(TB_surnameProperty, value); }
        }

  
        public static readonly DependencyProperty TB_surnameProperty =
            DependencyProperty.Register("TB_surname", typeof(string), typeof(ContactViewModel), new PropertyMetadata(""));



        public string TB_phone
        {
            get { return (string)GetValue(TB_phoneProperty); }
            set { SetValue(TB_phoneProperty, value); }
        }


        public static readonly DependencyProperty TB_phoneProperty =
            DependencyProperty.Register("TB_phone", typeof(string), typeof(ContactViewModel), new PropertyMetadata(""));



        public string TB_email
        {
            get { return (string)GetValue(TB_emailProperty); }
            set { SetValue(TB_emailProperty, value); }
        }


        public static readonly DependencyProperty TB_emailProperty =
            DependencyProperty.Register("TB_email", typeof(string), typeof(ContactViewModel), new PropertyMetadata(""));


    }
}
