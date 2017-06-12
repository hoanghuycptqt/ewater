using EWATER.DAL;
using EWATER.Models;
using EWATER.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EWATER.Services
{
    public partial class ClientOrderService
    {
        private GenericRepository<Order> OrderRepository;
        private GenericRepository<OrderItem> OrderItemRepository;
        public ClientOrderService()
        {
            this.OrderRepository = new GenericRepository<Order>(new EWaterContext());
            this.OrderItemRepository = new GenericRepository<OrderItem>(new EWaterContext());
        }

        public int InsertOrder(object[] parameters)
        {
            int flag;
            object[] order = { parameters[0], parameters[1], parameters[2], parameters[3] };

            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("INSERT INTO [Order] ( ");
            spQuery.Append("OrderID ");
            spQuery.Append(",CustomerID ");
            spQuery.Append(",OrderDate ");
            spQuery.Append(",Status) ");
            spQuery.Append("VALUES( ");
            spQuery.Append("{0} ");
            spQuery.Append(",{1} ");
            spQuery.Append(",{2} ");
            spQuery.Append(",{3})");

            flag = OrderRepository.ExecuteCommand(spQuery.ToString(), order);

            if(flag == 0)
            {
                return flag;
            }

            IList parameters4 = parameters[4] as IList;
            if (parameters4 != null)
            {
                foreach(object item in parameters4)
                {
                    System.Reflection.PropertyInfo OrderID = item.GetType().GetProperty("OrderID");
                    System.Reflection.PropertyInfo ProductID = item.GetType().GetProperty("ProductID");
                    System.Reflection.PropertyInfo Price = item.GetType().GetProperty("Price");
                    System.Reflection.PropertyInfo Quantity = item.GetType().GetProperty("Quantity");

                    String orderID = (String)(OrderID.GetValue(item, null));
                    int productID = (int)(ProductID.GetValue(item, null));
                    int price = (int)(Price.GetValue(item, null));
                    int quantity = (int)(Quantity.GetValue(item, null));

                    object[] orderItem = { orderID, productID, price, quantity };
                    StringBuilder spQueryItem = new StringBuilder();
                    spQueryItem.Append("INSERT INTO OrderItem ( ");
                    spQueryItem.Append("OrderID ");
                    spQueryItem.Append(",ProductID ");
                    spQueryItem.Append(",Price ");
                    spQueryItem.Append(",Quantity) ");
                    spQueryItem.Append("VALUES( ");
                    spQueryItem.Append("{0} ");
                    spQueryItem.Append(",{1} ");
                    spQueryItem.Append(",{2} ");
                    spQueryItem.Append(",{3})");

                    flag = OrderItemRepository.ExecuteCommand(spQueryItem.ToString(), orderItem);
                    spQueryItem.Clear();
                }
            }
               
            return flag;
        }

        public int Delete(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("DELETE FROM [Order] ");
            spQuery.Append("WHERE ");
            spQuery.Append("OrderID={0} ");

            return OrderRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }
    }
}