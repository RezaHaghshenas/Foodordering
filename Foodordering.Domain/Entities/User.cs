using Foodordering.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodordering.Domain.Entities
{
    public enum UserRole
    {
        Customer,
        Delivery,
        Admin
    }
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public DateTime RegisteredAt { get; private set; } = DateTime.UtcNow;
        public bool IsActive { get; private set; } = true;
        public string? ProfilePictureUrl { get; private set; }
        public string? Notes { get; private set; }
        public string SecurityStamp { get; private set; } = Guid.NewGuid().ToString();

        public List<DomainEvent> DomainEvents { get; private set; } = new List<DomainEvent>();

        public List<RefreshToken> RefreshTokens { get; private set; } = new();

        public List<PasswordResetCode> passwordResetCodes { get; private set; } = new();

        public List<Address> addresses { get; private set; } = new();     
        
        public List<LoginHistory> loginHistories { get; private set; } = new(); 

        private User() { } // EF Core

        public User(string name, string email, string phoneNumber, string passwordHash, UserRole role)
        {



            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
            if (!email.Contains("@")) throw new ArgumentException("Invalid email");


            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            Role = role;
            SecurityStamp = Guid.NewGuid().ToString();  

            DomainEvents.Add(new UserRegisteredEvent(Id, Email , PhoneNumber));
        }

        public void ChangePassword(string NewPasswordHash)
        { 
        PasswordHash = NewPasswordHash;
            SecurityStamp = Guid.NewGuid().ToString();
            DomainEvents.Add(new UserPasswordChangedEvent(Id));
        }


        public void ResetPassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            SecurityStamp = Guid.NewGuid().ToString();
            DomainEvents.Add(new UserPasswordResetEvent(Id));
        }



        public void Change_Security_Stamp()
        { 
            SecurityStamp = Guid.NewGuid().ToString();
            DomainEvents.Add(new UserSecurityStampChangedEvent(Id));
        }


        public void Deactivate()
        {
            IsActive = false;
            DomainEvents.Add(new UserDeactivatedEvent(Id));
        }

        public void ChangeRole(UserRole newRole)
        {
            if (Role != newRole)
            {
                Role = newRole;
                DomainEvents.Add(new UserRoleChangedEvent(Id, newRole));
            }
        }


        public void UpdateProfile(string? profilePictureUrl, string? notes)
        {
            if (!string.IsNullOrWhiteSpace(profilePictureUrl))
                ProfilePictureUrl = profilePictureUrl;

            if (!string.IsNullOrWhiteSpace(notes))
                Notes = notes;
        }

        public void ChangePhoneNumber(string newPhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(newPhoneNumber))
                throw new ArgumentException("Phone number cannot be empty.");

            if (PhoneNumber != newPhoneNumber)
            {
                PhoneNumber = newPhoneNumber;
                SecurityStamp = Guid.NewGuid().ToString(); // توکن‌های قبلی باطل میشن

                DomainEvents.Add(new UserPhoneNumberChangedEvent(Id, newPhoneNumber));
            }
        }


        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be empty.");

            if (Email != newEmail)
            {
                Email = newEmail;
                SecurityStamp = Guid.NewGuid().ToString(); // توکن‌های قبلی باطل میشن

                DomainEvents.Add(new UserEmailChangedEvent(Id, newEmail));
            }
        }


    }
}
