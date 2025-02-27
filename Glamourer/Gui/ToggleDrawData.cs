﻿using System;
using Glamourer.Designs;
using Glamourer.Events;
using Glamourer.State;
using Penumbra.GameData.Enums;

namespace Glamourer.Gui;

public ref struct ToggleDrawData
{
    public bool Locked;
    public bool DisplayApplication;

    public bool CurrentValue;
    public bool CurrentApply;

    public Action<bool> SetValue = null!;
    public Action<bool> SetApply = null!;

    public string Label   = string.Empty;
    public string Tooltip = string.Empty;

    public ToggleDrawData()
    { }

    public static ToggleDrawData FromDesign(ActorState.MetaIndex index, DesignManager manager, Design design)
    {
        var (label, value, apply, setValue, setApply) = index switch
        {
            ActorState.MetaIndex.HatState => ("Hat Visible", design.DesignData.IsHatVisible(), design.DoApplyHatVisible(),
                (Action<bool>)(b => manager.ChangeMeta(design, index, b)), (Action<bool>)(b => manager.ChangeApplyMeta(design, index, b))),
            ActorState.MetaIndex.VisorState => ("Visor Toggled", design.DesignData.IsVisorToggled(), design.DoApplyVisorToggle(),
                b => manager.ChangeMeta(design, index, b), b => manager.ChangeApplyMeta(design, index, b)),
            ActorState.MetaIndex.WeaponState => ("Weapon Visible", design.DesignData.IsWeaponVisible(), design.DoApplyWeaponVisible(),
                b => manager.ChangeMeta(design, index, b), b => manager.ChangeApplyMeta(design, index, b)),
            ActorState.MetaIndex.Wetness => ("Force Wetness", design.DesignData.IsWet(), design.DoApplyWetness(),
                b => manager.ChangeMeta(design, index, b), b => manager.ChangeApplyMeta(design, index, b)),
            _ => throw new Exception("Unsupported meta index."),
        };

        return new ToggleDrawData
        {
            Label              = label,
            Tooltip            = string.Empty,
            Locked             = design.WriteProtected(),
            DisplayApplication = true,
            CurrentValue       = value,
            CurrentApply       = apply,
            SetValue           = setValue,
            SetApply           = setApply,
        };
    }

    public static ToggleDrawData CrestFromDesign(CrestFlag slot, DesignManager manager, Design design)
        => new()
        {
            Label              = $"{slot.ToLabel()} Crest",
            Tooltip            = string.Empty,
            Locked             = design.WriteProtected(),
            DisplayApplication = true,
            CurrentValue       = design.DesignData.Crest(slot),
            CurrentApply       = design.DoApplyCrest(slot),
            SetValue           = v => manager.ChangeCrest(design, slot, v),
            SetApply           = v => manager.ChangeApplyCrest(design, slot, v),
        };

    public static ToggleDrawData CrestFromState(CrestFlag slot, StateManager manager, ActorState state)
        => new()
        {
            Label        = $"{slot.ToLabel()} Crest",
            Tooltip      = "Hide or show your free company crest on this piece of gear.",
            Locked       = state.IsLocked,
            CurrentValue = state.ModelData.Crest(slot),
            SetValue     = v => manager.ChangeCrest(state, slot, v, StateChanged.Source.Manual),
        };

    public static ToggleDrawData FromState(ActorState.MetaIndex index, StateManager manager, ActorState state)
    {
        var (label, tooltip, value, setValue) = index switch
        {
            ActorState.MetaIndex.HatState => ("Hat Visible", "Hide or show the characters head gear.", state.ModelData.IsHatVisible(),
                (Action<bool>)(b => manager.ChangeHatState(state, b, StateChanged.Source.Manual))),
            ActorState.MetaIndex.VisorState => ("Visor Toggled", "Toggle the visor state of the characters head gear.",
                state.ModelData.IsVisorToggled(),
                b => manager.ChangeVisorState(state, b, StateChanged.Source.Manual)),
            ActorState.MetaIndex.WeaponState => ("Weapon Visible", "Hide or show the characters weapons when not drawn.",
                state.ModelData.IsWeaponVisible(),
                b => manager.ChangeWeaponState(state, b, StateChanged.Source.Manual)),
            ActorState.MetaIndex.Wetness => ("Force Wetness", "Force the character to be wet or not.", state.ModelData.IsWet(),
                b => manager.ChangeWetness(state, b, StateChanged.Source.Manual)),
            _ => throw new Exception("Unsupported meta index."),
        };

        return new ToggleDrawData
        {
            Label        = label,
            Tooltip      = tooltip,
            Locked       = state.IsLocked,
            CurrentValue = value,
            SetValue     = setValue,
        };
    }

    public static ToggleDrawData FromValue(ActorState.MetaIndex index, bool value)
    {
        var (label, tooltip) = index switch
        {
            ActorState.MetaIndex.HatState    => ("Hat Visible", "Hide or show the characters head gear."),
            ActorState.MetaIndex.VisorState  => ("Visor Toggled", "Toggle the visor state of the characters head gear."),
            ActorState.MetaIndex.WeaponState => ("Weapon Visible", "Hide or show the characters weapons when not drawn."),
            ActorState.MetaIndex.Wetness     => ("Force Wetness", "Force the character to be wet or not."),
            _                                => throw new Exception("Unsupported meta index."),
        };
        return new ToggleDrawData
        {
            Label        = label,
            Tooltip      = tooltip,
            Locked       = true,
            CurrentValue = value,
        };
    }
}
