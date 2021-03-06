﻿using BakerSoft.Models;
using GSTBill.ViewModels;
using log4net;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakerSoft.ViewModels
{
    class AddSupplierViewModel : BaseViewModel
    {
        private static readonly ILog log = LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private SupplierModel _supplierModel;

        private List<Supplier> _supplierList;
        public List<Supplier> SupplierList
        {
            get { return _supplierList; }
            set
            {
                SetProperty(ref _supplierList, value);
            }
        }
        private string _supplierName;
        public string SupplierName
        {
            get { return _supplierName; }
            set { SetProperty(ref _supplierName, value); }
        }
        private string _gstNumber;
        public string GstNumber
        {
            get { return _gstNumber; }
            set { SetProperty(ref _gstNumber, value); }
        }
        private string _tinNumber;
        public string TinNumber
        {
            get { return _tinNumber; }
            set { SetProperty(ref _tinNumber, value); }
        }
        private string _addressLine1;
        public string AddressLine1
        {
            get { return _addressLine1; }
            set { SetProperty(ref _addressLine1, value); }
        }
        private string _addressLine2;
        public string AddressLine2
        {
            get { return _addressLine2; }
            set { SetProperty(ref _addressLine2, value); }
        }
        private string _addressLine3;
        public string AddressLine3
        {
            get { return _addressLine3; }
            set { SetProperty(ref _addressLine3, value); }
        }
        private string _city;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }
        private string _state;
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }
        private string _pincode;
        public string Pincode
        {
            get { return _pincode; }
            set { SetProperty(ref _pincode, value); }
        }
        public InteractionRequest<INotification> NotificationRequest { get; private set; }

        private DelegateCommand _addSupplierCmd;
        public DelegateCommand AddSupplierCmd
        {
            get { return _addSupplierCmd; }
            set
            {
                SetProperty(ref _addSupplierCmd, value);
            }
        }

        public AddSupplierViewModel(SupplierModel supplierModel)
        {
            _supplierModel = supplierModel;
            SupplierList = _supplierModel.GetSuppliers();
            AddSupplierCmd = new DelegateCommand(AddSupplier);
            NotificationRequest = new InteractionRequest<INotification>();
        }

        private void RaiseNotification(string title, string message)
        {
            this.NotificationRequest.Raise(
               new Notification { Content = message, Title = title });
        }

        private void AddSupplier()
        {
            if(!ValidateInputFields())
            {
                RaiseNotification("Alert", "Please fill all the required fields");
                return;
            }

            var supplier = new Supplier();
            var address = new Address();
            try
            {
                supplier.SupplierGST = GstNumber;
                supplier.SupplierName = SupplierName;
                supplier.SupplierTIN = TinNumber;

                address.AddressLine1 = AddressLine1;
                address.AddressLine2 = AddressLine2;
                address.AddressLine3 = AddressLine3;
                address.City = City;
                address.State = State;
                address.Pincode = Pincode;
                supplier.ADDRESS = address;

                _supplierModel.AddSupplier(supplier);
                log.Info(String.Format("New Supplier {0} added successfully", SupplierName));
            }
            catch (Exception ex)
            {
                log.Error(supplier, ex);
                RaiseNotification("Error", "Failed to complete the operation");
            }
            finally
            {
                RaiseNotification("Success", string.Format("Successfully added new supplier '{0}'", SupplierName));
                ResetUI();
            }
        }

        private void ResetUI()
        {
            GstNumber = "";
            SupplierName = "";
            TinNumber = "";
            AddressLine1 = "";
            AddressLine2 = "";
            AddressLine3 = "";
            City = "";
            State = "";
            Pincode = "";
            SupplierList = _supplierModel.GetSuppliers();
        }

        private bool ValidateInputFields()
        {
            if (string.IsNullOrWhiteSpace(SupplierName)||
                string.IsNullOrWhiteSpace(GstNumber)||
                string.IsNullOrWhiteSpace(AddressLine1)||
                string.IsNullOrWhiteSpace(City)||
                string.IsNullOrWhiteSpace(State))
                return false;
            return true;
        }
    }
}
