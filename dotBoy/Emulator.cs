﻿using System;

using DotBoy.Core;
using DotBoy.Core.Logging;
using DotBoy.Interfaces;

namespace DotBoy
{
    public static class Emulator
    {
        public static void Init(Rom rom)
        {
            if (rom.Information.Type != CartridgeTypeId.RomOnly)
            {
                Console.Error.WriteLine("Unsupported cartridge type");
                Environment.Exit(1);
            }

            IClock clock = new Clock();
            IMemory memory = new LoggedMemory(new Memory());
            IRegisters registers = new LoggedRegisters(new Registers());
            IPipeline pipeline = new Pipeline();

            memory.BlockLoad(
                rom.Content,
                0,
                0,
                32 * 1024);

            registers.PC = 0x0100;

            ICpu cpu = new Cpu(clock, memory, registers, pipeline);

            try
            {
                cpu.StartSynchronousExecution();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
