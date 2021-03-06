﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Pages
{
    public partial class AddOrderDetailComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected NorthwindService Northwind { get; set; }

        IEnumerable<NorthwindBlazor.Models.Northwind.Order> _getOrdersResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Order> getOrdersResult
        {
            get
            {
                return _getOrdersResult;
            }
            set
            {
                if(!object.Equals(_getOrdersResult, value))
                {
                    _getOrdersResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<NorthwindBlazor.Models.Northwind.Product> _getProductsResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Product> getProductsResult
        {
            get
            {
                return _getProductsResult;
            }
            set
            {
                if(!object.Equals(_getProductsResult, value))
                {
                    _getProductsResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        NorthwindBlazor.Models.Northwind.OrderDetail _orderdetail;
        protected NorthwindBlazor.Models.Northwind.OrderDetail orderdetail
        {
            get
            {
                return _orderdetail;
            }
            set
            {
                if(!object.Equals(_orderdetail, value))
                {
                    _orderdetail = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var northwindGetOrdersResult = await Northwind.GetOrders();
            getOrdersResult = northwindGetOrdersResult;

            var northwindGetProductsResult = await Northwind.GetProducts();
            getProductsResult = northwindGetProductsResult;

            orderdetail = new NorthwindBlazor.Models.Northwind.OrderDetail();
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.OrderDetail args)
        {
            try
            {
                var northwindCreateOrderDetailResult = await Northwind.CreateOrderDetail(orderdetail);
                DialogService.Close(orderdetail);
            }
            catch (Exception northwindCreateOrderDetailException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new OrderDetail!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
