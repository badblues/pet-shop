using System.Data;
using System.Data.Common;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using Persistence.Models;

namespace Persistence.UserTypes;

public class GenderType : IUserType
{
    public SqlType[] SqlTypes => new[] { new SqlType(DbType.String) };

    public Type ReturnedType => typeof(Gender);

    public bool IsMutable => false;

    public object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
    {
        string value = (string)NHibernateUtil.String.NullSafeGet(rs, names, session, owner);
        return value == null ? null : Enum.Parse(typeof(Gender), value);
    }

    public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        IDataParameter parameter = cmd.Parameters[index];
        parameter.Value = value;
    }

    public object Assemble(object cached, object owner)
    {
        return cached;
    }

    public object DeepCopy(object value)
    {
        return value;
    }

    public object Disassemble(object value)
    {
        return value;
    }

    public new bool Equals(object x, object y)
    {
        return ReferenceEquals(x, y) || (x != null && y != null && x.Equals(y));
    }

    public int GetHashCode(object x)
    {
        return x?.GetHashCode() ?? 0;
    }

    public object Replace(object original, object target, object owner)
    {
        return original;
    }
}
