using System;

public interface ICharacter
{
   
    string Name { get; set; }

   
    event Action OnDeath;

   
    void Interact(int amount);
}
