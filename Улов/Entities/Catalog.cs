namespace Улов.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Windows;
    using System.Windows.Media;

    [Table("Catalog")]
    public partial class Catalog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Article { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int UnitId { get; set; }

        public double Cost { get; set; }

        public double MaxSale { get; set; }

        public int ManufactureId { get; set; }

        public int ProviderId { get; set; }

        public int CategoryId { get; set; }

        public double CurrentSale { get; set; }

        public int Count { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        public byte[] Photo { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacture Manufacture { get; set; }

        public virtual Provider Provider { get; set; }

        public virtual Unit Unit { get; set; }

        [NotMapped]
        public double DiscountCost => Cost - (Cost / 100) * CurrentSale;

        [NotMapped]
        public Visibility DiscountCostVisibility => CurrentSale == 0 ? Visibility.Collapsed : Visibility.Visible;

        [NotMapped]
        public SolidColorBrush DiscountBackground
        {
            get
            {
                if (CurrentSale >= 15)
                {
                    var color = (Color)ColorConverter.ConvertFromString("#7fff00");
                    return new SolidColorBrush(color);
                }

                    return new SolidColorBrush(Colors.White);
            }
        }
    }
}
