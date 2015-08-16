/// <summary>
/// Mapping Helper Class.
/// </summary>
public class DataTableHelper
{
    /// <summary>
    /// Datas the table to entities.
    /// </summary>
    /// <typeparam name="T"><peparam>
    /// <param name="dt">The dt.</param>
    /// <returns>The List.</returns>
    public static IEnumerable<T> DataTableToEntities<T>(DataTable dt)
    {
        foreach (DataRow row in dt.Rows)
        {
            T result = Activator.CreateInstance<T>();
            foreach (DataColumn column in dt.Columns)
            {
                var property = typeof(T).GetProperty(column.ColumnName);
                if (property != null)
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(result, DbNullToNull(row[column.ColumnName]), null);
                    }
                }
            }
            yield return result;
        }
    }

    /// <summary>
    /// 将集合类转换成DataTable
    /// </summary>
    /// <param name="list">集合</param>
    /// <returns></returns>
    public static DataTable EntitiesToDataTable(IList list)
    {
        DataTable result = new DataTable();
        if (list.Count > 0)
        {
            PropertyInfo[] propertys = list[0].GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                result.Columns.Add(pi.Name, pi.PropertyType);
            }

            for (int i = 0; i < list.Count; i++)
            {
                var tempList = new ArrayList();
                foreach (PropertyInfo pi in propertys)
                {
                    object obj = pi.GetValue(i, null);
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                result.LoadDataRow(array, true);
            }
        }
        return result;
    }

    /// <summary>
    /// Datas the row to entity.
    /// </summary>
    /// <typeparam name="T"><peparam>
    /// <param name="dr">The dr.</param>
    /// <returns>The entity.</returns>
    public static T DataRowToEntity<T>(DataRow dr)
    {
        T result = Activator.CreateInstance<T>();
        foreach (DataColumn column in dr.Table.Columns)
        {
            var property = typeof(T).GetProperty(column.ColumnName);
            if (property != null)
            {
                if (property.CanWrite)
                {
                    if (property.PropertyType.FullName == typeof(Int64).ToString())
                    {
                        property.SetValue(result, (DbNullToNull(dr[column.ColumnName]) == null ? 0 : Convert.ToInt64(dr[column.ColumnName])), null);
                    }
                    else
                    {
                        property.SetValue(result, DbNullToNull(dr[column.ColumnName]), null);
                    }
                }
            }
        }
        return result;
    }

    /// <summary>
    /// Databases the null to null.
    /// </summary>
    /// <param name="original">The original.</param>
    /// <returns>The result.</returns>
    public static object DbNullToNull(object original)
    {
        return original == DBNull.Value ? null : original;
    }
}