using System.Reflection;
using System.Text;

namespace Study_TBF_Stats.Extensions.Utility
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            // Tách các tham số sắp xếp từ chuỗi đầu vào
            var orderParams = orderByQueryString.Trim().Split(',');
            // Lấy thông tin về các thuộc tính của kiểu T

            var propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // StringBuilder để xây dựng câu truy vấn sắp xếp
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                // Lấy tên thuộc tính từ tham số
                var propertyFromQuery = param.Split(" ")[0];
                // Tìm thuộc tính tương ứng trong kiểu T

                var objectProperty = propertyInfo.FirstOrDefault(pi => pi.Name.Equals(propertyFromQuery, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;
                // Xác định hướng sắp xếp (ascending hoặc descending)

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return orderQuery;
        }
    }
}
