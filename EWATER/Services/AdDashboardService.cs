using EWATER.DAL;
using EWATER.Entity;
using EWATER.Models;
using EWATER.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace EWATER.Services
{
    public partial class AdDashboardService
    {
        private GenericRepository<OrderRecent> OrderRecentRepository;
        private GenericRepository<YearChart> YearChartRepository;
        private GenericRepository<MonthChart> MonthChartRepository;
        private GenericRepository<Order> OrderRepository;
        private GenericRepository<Staff> StaffRepository;
        private GenericRepository<ProductAdd> ProductRepository;
        private GenericRepository<Models.OrderItem> OrderItemRepository;
        private GenericRepository<AllTotal> OrderTotalRepository;
        public AdDashboardService()
        {
            this.MonthChartRepository = new GenericRepository<MonthChart>(new EWaterContext());
            this.YearChartRepository = new GenericRepository<YearChart>(new EWaterContext());
            this.OrderRecentRepository = new GenericRepository<OrderRecent>(new EWaterContext());
            this.OrderRepository = new GenericRepository<Order>(new EWaterContext());
            this.StaffRepository = new GenericRepository<Staff>(new EWaterContext());
            this.ProductRepository = new GenericRepository<ProductAdd>(new EWaterContext());
            this.OrderItemRepository = new GenericRepository<Models.OrderItem>(new EWaterContext());
            this.OrderTotalRepository = new GenericRepository<AllTotal>(new EWaterContext());
        }
        public IEnumerable<OrderRecent> GetAll(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT a.OrderID,b.CustomerName,e.StaffName,a.OrderDate,a.Status,c.TotalPrice,a.StaffID,b.PhoneNumber,b.Address,f.ProductName,d.Price,d.Quantity ");
            spQuery.Append("FROM [Order] a ");
            spQuery.Append("INNER JOIN Customer b ");
            spQuery.Append("on a.CustomerID = b.CustomerID ");
            spQuery.Append("INNER JOIN ( ");
            spQuery.Append("SELECT OrderID, SUM(Price*Quantity) AS TotalPrice ");
            spQuery.Append("FROM OrderItem ");
            spQuery.Append("GROUP BY OrderID) c ");
            spQuery.Append("on a.OrderID = c.OrderID ");
            spQuery.Append("INNER JOIN OrderItem d ");
            spQuery.Append("on a.OrderID=d.OrderID ");
            spQuery.Append("INNER JOIN Product f ");
            spQuery.Append("on d.ProductID = f.ProductID ");
            spQuery.Append("LEFT JOIN Staff e ");
            spQuery.Append("on a.StaffID = e.StaffID ");
            spQuery.Append("ORDER BY a.OrderDate DESC ");

            return OrderRecentRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }

        public AllTotal GetAllTotal(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT T1.TotalSale, T2.TotalOrder, T3.TotalCustomer ");
            spQuery.Append("FROM ( ");
            spQuery.Append("SELECT SUM(c.TotalPrice) AS TotalSale ");
            spQuery.Append("FROM( ");
            spQuery.Append("SELECT a.OrderID, SUM(b.Price * b.Quantity) AS TotalPrice ");
            spQuery.Append("FROM [Order] a ");
            spQuery.Append("INNER JOIN OrderItem b ");
            spQuery.Append("on a.OrderID = b.OrderID ");
            spQuery.Append("WHERE a.Status = 'true' ");
            spQuery.Append("GROUP BY a.OrderID) c ");
            spQuery.Append(") as T1, ");
            spQuery.Append("( ");
            spQuery.Append("SELECT COUNT(OrderID) AS TotalOrder ");
            spQuery.Append("FROM [Order] ");
            spQuery.Append(") as T2, ");
            spQuery.Append("( ");
            spQuery.Append("SELECT COUNT(CustomerID) AS TotalCustomer ");
            spQuery.Append("FROM Customer ");
            spQuery.Append(") as T3 ");

            return OrderTotalRepository.ExecuteQuerySingle(spQuery.ToString(), parameters);
        }

        public IEnumerable<Staff> GetAllStaff(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT * ");
            spQuery.Append("FROM Staff ");

            return StaffRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }

        public IEnumerable<ProductAdd> GetAllProduct(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT ProductID,ProductName,Price ");
            spQuery.Append("FROM Product ");

            return ProductRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }

        public int Update(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("UPDATE [Order] SET ");
            spQuery.Append("StaffID={1} ");
            spQuery.Append(",Status={2} ");
            spQuery.Append("WHERE ");
            spQuery.Append("OrderID={0}");

            return OrderRepository.ExecuteCommand(spQuery.ToString(), parameters);
        }

        public IEnumerable<YearChart> GetYearChart(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("SELECT m.MONTH as Month,isnull((n.moneyofmonth), 0) as Sum ");
            spQuery.Append("FROM ( ");
            spQuery.Append("SELECT 'January' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'February' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'March' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'April' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'May' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'June' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'July' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'August' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'September' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'October' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'November' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append("UNION SELECT 'December' AS ");
            spQuery.Append("MONTH ");
            spQuery.Append(") AS m ");
            spQuery.Append("LEFT JOIN ( ");
            spQuery.Append("SELECT DateName( month , DateAdd( month , CAST(CAST(MONTH(a.OrderDate) AS VARCHAR(2)) as int) , -1 ) )  as months,SUM(b.TotalPrice) as moneyofmonth ");
            spQuery.Append("FROM [Order] a ");
            spQuery.Append("INNER JOIN ( ");
            spQuery.Append("SELECT OrderID, SUM(Price*Quantity) AS TotalPrice  ");
            spQuery.Append("FROM OrderItem  ");
            spQuery.Append("GROUP BY OrderID ");
            spQuery.Append(") b ");
            spQuery.Append("on a.OrderID = b.OrderID ");
            spQuery.Append("WHERE a.Status = 'True' ");
            spQuery.Append("and Year(a.OrderDate) = {0} ");
            spQuery.Append("GROUP BY CAST(MONTH(a.OrderDate) AS VARCHAR(2))) n ");
            spQuery.Append("on m.MONTH=n.months ");
            spQuery.Append("ORDER BY DATEPART(MM,m.MONTH+'01 2011') ");

            return YearChartRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }

        public IEnumerable<MonthChart> GetMonthChart(object[] parameters)
        {
            StringBuilder spQuery = new StringBuilder();
            spQuery.Append("DECLARE @month AS INT = {0} ");
            spQuery.Append("DECLARE @Year AS INT = {1} ");
            spQuery.Append(";WITH N(N)AS ");
            spQuery.Append("(SELECT 1 FROM(VALUES(1),(1),(1),(1),(1),(1))M(N)), ");
            spQuery.Append("tally(N)AS(SELECT ROW_NUMBER()OVER(ORDER BY N.N)FROM N,N a) ");
            spQuery.Append("SELECT m.day as Days, isnull( n.moneyofday,0) as Sum ");
            spQuery.Append("FROM ( ");
            spQuery.Append("SELECT N day FROM tally ");
            spQuery.Append("WHERE N <= day(EOMONTH(datefromparts(@year,@month,1))) ");
            spQuery.Append(") m ");
            spQuery.Append("left join ( ");
            spQuery.Append("SELECT day(a.OrderDate) as days,SUM(b.TotalPrice) as moneyofday ");
            spQuery.Append("FROM [Order] a  ");
            spQuery.Append("INNER JOIN ( ");
            spQuery.Append("SELECT OrderID, SUM(Price*Quantity) AS TotalPrice ");
            spQuery.Append("FROM OrderItem  ");
            spQuery.Append("GROUP BY OrderID ");
            spQuery.Append(") b ");
            spQuery.Append("on a.OrderID = b.OrderID ");
            spQuery.Append("WHERE a.Status = 'True' ");
            spQuery.Append("and month(a.OrderDate) = {0} ");
            spQuery.Append("GROUP BY day(a.OrderDate) ");
            spQuery.Append(") n ");
            spQuery.Append("on m.day = n.days ");

            return MonthChartRepository.ExecuteQuery(spQuery.ToString(), parameters);
        }
    }
}