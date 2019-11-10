using System;
using System.Collections.Generic;
using System.Text;
using ReloadedRPCS3TestsNoConfig.RPCS3;

namespace ReloadedRPCS3TestsNoConfig.Games
{
    public abstract class GameBindings
    {
        public RPCS3Bindings Bindings { get; private set; }

        protected GameBindings( RPCS3Bindings bindings )
        {
            Bindings = bindings;
        }

        /// <summary>
        /// Helper function for register PPU functions.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected PPUFunction RegisterPPUFunction( uint address, string name = null )
        {
            if ( !Bindings.PPUFunctions.TryGetValue( address, out var function ) )
            {
                throw new InvalidOperationException( $"No PPU function registered at 0x{address:X8} while trying to register {name}" );
            }

            return new PPUFunction( function, address );
        }

        /// <summary>
        /// Called after a game is loaded.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Called when any hooks should be activated.
        /// </summary>
        public abstract void Activate();

        /// <summary>
        /// Called when any hooks should be resumed.
        /// </summary>
        public abstract void Resume();

        /// <summary>
        /// Called when any hooks should be suspended.
        /// </summary>
        public abstract void Suspend();

        /// <summary>
        /// Called when any hooks should be unloaded.
        /// </summary>
        public abstract void Unload();
    }
}
