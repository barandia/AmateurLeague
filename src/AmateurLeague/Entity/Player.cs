using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace AmateurLeague.Entity
{
    public enum GenderType
    {
        Male,
        Female
    }

    /**
     * A player can be on one team per league
     */
    //[DataContract(IsReference = true)]
    public class Player
    {
        public int PlayerId { get; set; }
        [EmailAddress(ErrorMessage = "The Email Address is not valid")]
        [Required(ErrorMessage = "Please enter an email address.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter the player's first name.")]
        [StringLength(50, ErrorMessage = "The First Name must be less than {1} characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the player's last name.")]
        [StringLength(50, ErrorMessage = "The Last Name must be less than {1} characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "Please enter the player's date of birth.")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }

        public override string ToString()
        {
            var strBldr = new StringBuilder();
            strBldr.Append($"{{\"PlayerId\": \"{PlayerId}\",");
            strBldr.Append($"\"EmailAddress\": \"{EmailAddress}\",");
            strBldr.Append($"\"FirstName\": \"{FirstName}\",");
            strBldr.Append($"\"LastName\": \"{LastName}\",");
            strBldr.Append($"\"Gender\": \"{Gender}\",");
            strBldr.Append($"\"DateOfBirth\": \"{DateOfBirth}\"}}");

            return strBldr.ToString();
        }
    }
}