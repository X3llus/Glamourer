﻿using System;
using System.Linq;
using System.Numerics;
using Dalamud.Interface;
using Glamourer.GameData;
using Glamourer.Designs;
using Glamourer.Events;
using Glamourer.Interop;
using Glamourer.Interop.Structs;
using Glamourer.State;
using ImGuiNET;
using OtterGui;
using OtterGui.Raii;
using Penumbra.GameData.Enums;
using Penumbra.GameData.Gui.Debug;

namespace Glamourer.Gui.Tabs.DebugTab;

public class ActiveStatePanel(StateManager _stateManager, ObjectManager _objectManager) : IGameDataDrawer
{
    public string Label
        => $"Active Actors ({_stateManager.Count})###Active Actors";

    public bool Disabled
        => false;

    public void Draw()
    {
        _objectManager.Update();
        foreach (var (identifier, actors) in _objectManager)
        {
            if (ImGuiUtil.DrawDisabledButton($"{FontAwesomeIcon.Trash.ToIconString()}##{actors.Label}", new Vector2(ImGui.GetFrameHeight()),
                    string.Empty, !_stateManager.ContainsKey(identifier), true))
                _stateManager.DeleteState(identifier);

            ImGui.SameLine();
            using var t = ImRaii.TreeNode(actors.Label);
            if (!t)
                continue;

            if (_stateManager.GetOrCreate(identifier, actors.Objects[0], out var state))
                DrawState(_stateManager, actors, state);
            else
                ImGui.TextUnformatted("Invalid actor.");
        }
    }

    public static void DrawState(StateManager stateManager, ActorData data, ActorState state)
    {
        using var table = ImRaii.Table("##state", 7, ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit);
        if (!table)
            return;

        ImGuiUtil.DrawTableColumn("Name");
        ImGuiUtil.DrawTableColumn(state.Identifier.ToString());
        ImGui.TableNextColumn();
        if (ImGui.Button("Reset"))
            stateManager.ResetState(state, StateChanged.Source.Manual);

        ImGui.TableNextRow();

        static void PrintRow<T>(string label, T actor, T model, StateChanged.Source source) where T : notnull
        {
            ImGuiUtil.DrawTableColumn(label);
            ImGuiUtil.DrawTableColumn(actor.ToString()!);
            ImGuiUtil.DrawTableColumn(model.ToString()!);
            ImGuiUtil.DrawTableColumn(source.ToString());
        }

        static string ItemString(in DesignData data, EquipSlot slot)
        {
            var item = data.Item(slot);
            return $"{item.Name} ({item.PrimaryId.Id}{(item.SecondaryId != 0 ? $"-{item.SecondaryId.Id}" : string.Empty)}-{item.Variant})";
        }

        PrintRow("Model ID", state.BaseData.ModelId, state.ModelData.ModelId, state[ActorState.MetaIndex.ModelId]);
        ImGui.TableNextRow();
        PrintRow("Wetness", state.BaseData.IsWet(), state.ModelData.IsWet(), state[ActorState.MetaIndex.Wetness]);
        ImGui.TableNextRow();

        if (state.BaseData.IsHuman && state.ModelData.IsHuman)
        {
            PrintRow("Hat Visible", state.BaseData.IsHatVisible(), state.ModelData.IsHatVisible(), state[ActorState.MetaIndex.HatState]);
            ImGui.TableNextRow();
            PrintRow("Visor Toggled", state.BaseData.IsVisorToggled(), state.ModelData.IsVisorToggled(),
                state[ActorState.MetaIndex.VisorState]);
            ImGui.TableNextRow();
            PrintRow("Weapon Visible", state.BaseData.IsWeaponVisible(), state.ModelData.IsWeaponVisible(),
                state[ActorState.MetaIndex.WeaponState]);
            ImGui.TableNextRow();
            foreach (var slot in EquipSlotExtensions.EqdpSlots.Prepend(EquipSlot.OffHand).Prepend(EquipSlot.MainHand))
            {
                PrintRow(slot.ToName(), ItemString(state.BaseData, slot), ItemString(state.ModelData, slot), state[slot, false]);
                ImGuiUtil.DrawTableColumn(state.BaseData.Stain(slot).Id.ToString());
                ImGuiUtil.DrawTableColumn(state.ModelData.Stain(slot).Id.ToString());
                ImGuiUtil.DrawTableColumn(state[slot, true].ToString());
            }

            foreach (var type in Enum.GetValues<CustomizeIndex>())
            {
                PrintRow(type.ToDefaultName(), state.BaseData.Customize[type].Value, state.ModelData.Customize[type].Value, state[type]);
                ImGui.TableNextRow();
            }
        }
        else
        {
            ImGuiUtil.DrawTableColumn(string.Join(" ", state.BaseData.GetCustomizeBytes().Select(b => b.ToString("X2"))));
            ImGuiUtil.DrawTableColumn(string.Join(" ", state.ModelData.GetCustomizeBytes().Select(b => b.ToString("X2"))));
            ImGui.TableNextRow();
            ImGuiUtil.DrawTableColumn(string.Join(" ", state.BaseData.GetEquipmentBytes().Select(b => b.ToString("X2"))));
            ImGuiUtil.DrawTableColumn(string.Join(" ", state.ModelData.GetEquipmentBytes().Select(b => b.ToString("X2"))));
        }
    }
}
