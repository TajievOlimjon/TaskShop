using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Customer
    {   
        public int Id { get; set; }

        [Display(Name ="Имя"),Required(ErrorMessage = "Пожолуйста ведите  имя? ")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия"),Required(ErrorMessage = "Пожолуйста ведите фамилию ?")]
        public string LastName { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = " Номер телефона"), Required(ErrorMessage = "Пожолуйста ведите номер телефона? ")]
        public string PhoneNumber { get; set; }

        [Display(Name = " Электронная почта"), Required(ErrorMessage = "Пожолуйста ведите электронную почту ?")]
        public string Email { get; set; }

        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

        [Display(Name = "Диапазон рассрочки: 3-6-9-12-18-24 месяцев." +
            " Смартфон: рассрочка до 9 месяцев бе каких либо % \n" +
            "Компьютер: рассрочка 12 месяцев бе каких либо % \n" +
            "Телевизор: рассрочка 18 месяцев бе каких либо % \n" +
            "за каждую допольнительную месац добавляется проценты от общей суммы продукта:\n" +
            " 3% для смартфонов, 4% для компьютеров и 5% для телевизоров"), 
            Required(ErrorMessage = "Ведите диапазон  ")]
        [Range(3,24,ErrorMessage ="Недопустимый диапазон ")]
        public int InstallmentRange { get; set; }
       





    }
}
