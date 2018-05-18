/*
 * Almudena López Sánchez 2018
 * 
 * 0.01, 14-May-2018: Initial version, based on Saboteur and Gauntlet
 */

class Screen
{
    protected Hardware hardware;

    public Screen(Hardware hardware)
    {
        this.hardware = hardware;
    }

    public virtual void Run()
    {
        // To be redefined in subclasses
    }
}
