using System.ComponentModel;

namespace Restaurant.Domain.Enums
{
    public enum OrderStatus
    {   
        [Description("Preparo Iniciado")]
        Started,

        [Description("Preparo finalizado")]
        Finished,

        [Description("Pedido entregue")]
        Delivered
    }
}
