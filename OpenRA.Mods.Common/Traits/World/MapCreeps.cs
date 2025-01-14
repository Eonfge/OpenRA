#region Copyright & License Information
/*
 * Copyright (c) The OpenRA Developers and Contributors
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System.Collections.Generic;
using OpenRA.Traits;

namespace OpenRA.Mods.Common.Traits
{
	[Desc("Controls the 'Creeps' checkbox in the lobby options.")]
	[TraitLocation(SystemActors.World)]
	public class MapCreepsInfo : TraitInfo, ILobbyOptions
	{
		[TranslationReference]
		[Desc("Descriptive label for the creeps checkbox in the lobby.")]
		public readonly string CheckboxLabel = "dropdown-map-creeps.label";

		[TranslationReference]
		[Desc("Tooltip description for the creeps checkbox in the lobby.")]
		public readonly string CheckboxDescription = "dropdown-map-creeps.description";

		[Desc("Default value of the creeps checkbox in the lobby.")]
		public readonly bool CheckboxEnabled = true;

		[Desc("Prevent the creeps state from being changed in the lobby.")]
		public readonly bool CheckboxLocked = false;

		[Desc("Whether to display the creeps checkbox in the lobby.")]
		public readonly bool CheckboxVisible = true;

		[Desc("Display order for the creeps checkbox in the lobby.")]
		public readonly int CheckboxDisplayOrder = 0;

		IEnumerable<LobbyOption> ILobbyOptions.LobbyOptions(MapPreview map)
		{
			yield return new LobbyBooleanOption("creeps", CheckboxLabel, CheckboxDescription, CheckboxVisible, CheckboxDisplayOrder, CheckboxEnabled, CheckboxLocked);
		}

		public override object Create(ActorInitializer init) { return new MapCreeps(this); }
	}

	public class MapCreeps : INotifyCreated
	{
		readonly MapCreepsInfo info;
		public bool Enabled { get; private set; }

		public MapCreeps(MapCreepsInfo info)
		{
			this.info = info;
		}

		void INotifyCreated.Created(Actor self)
		{
			Enabled = self.World.LobbyInfo.GlobalSettings
				.OptionOrDefault("creeps", info.CheckboxEnabled);
		}
	}
}
