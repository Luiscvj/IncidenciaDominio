namespace IncApi.Helpers;

public class Autorizacion
{
    public enum Roles
    {
        Administrador,
        Gerente,
        Empleado,
        Trainer,
        Estudiante
    }

    public const Roles rol_predeterminado = Roles.Empleado;
}