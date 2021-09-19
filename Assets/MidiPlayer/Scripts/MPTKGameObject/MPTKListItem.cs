using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiPlayerTK
{
    /// <summary>
    /// This class is useful when a list of paired value string+id is needed.\n
    /// This is also the entry point to display a popup for selecting a value by user: midi, preset, bank, drum, generator, ...
    /// </summary>
    public class MPTKListItem
    {
        /// <summary>
        /// Index associated to the label (not to mix up with Position in list): 
        ///! @li @c Patch number if patch list, 
        ///! @li @c Bank number if bank list, 
        ///! @li @c Midi Index for selecting a Midi from the MidiDB.
        ///! @li @c Generator Id for selecting a generator to apply.
        /// </summary>
        public int Index;

        /// <summary>
        /// Label associated to the index.
        ///! @li @c Patch Label if patch list (Piano, Violin, ...), 
        ///! @li @c Midi File Name for selecting a Midi from the MidiDB.
        ///! @li @c Generator Name for selecting a generator to apply.
        /// </summary>
        public string Label;

        /// <summary>
        /// Position in a list (not to mix up with Index which is a value associated to the Label)
        /// </summary>
        public int Position;
    }
}
