using Clayzor.Lib.Entities;

namespace Kesco.App.Web.Users.Models;

/// <summary>
/// Read-only строка сотрудника для серверного ClayGrid на форме поиска.
/// </summary>
public sealed class EmployeeSearchEntity : Entity
{
    /// <summary>Идентификатор сотрудника.</summary>
    public override int Id { get; set; }

    /// <summary>Отображаемое имя сотрудника.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Имя сотрудника на английском языке.</summary>
    public string? NameEn { get; set; }

    /// <summary>Общий запрос сотрудников без WHERE и ORDER BY.</summary>
    protected override string SelectSql => SQLQueries.SELECT_СотрудникиДляПоиска;

    /// <summary>Операция добавления не поддерживается read-only моделью.</summary>
    protected override string InsertSql => throw new NotSupportedException();

    /// <summary>Операция обновления не поддерживается read-only моделью.</summary>
    protected override string UpdateSql => throw new NotSupportedException();

    /// <summary>Операция удаления не поддерживается read-only моделью.</summary>
    protected override string DeleteSql => throw new NotSupportedException();
}
