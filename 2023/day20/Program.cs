using System.Diagnostics;

var lines = File.ReadAllLines("input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .ToList();

var sw = new Stopwatch();
sw.Start();

CommunicationMachine machine = new();
foreach (var line in lines)
{
    var parts = line.Split(" -> ");
    machine.RegisterModule(parts[0], parts[1].Split(", "));
}
machine.InitializeModules();
machine.Start();
var part1 = machine.GetTotalPulseValue();
Console.WriteLine($"Part 1: {part1}");


var i = 1L;
foreach (var c in machine.Part2Counters.Values)
{
    i = LCM(i, c);
}
Console.WriteLine($"Part 2: {i}");

sw.Stop();
Console.WriteLine($"Total execution time: {sw.Elapsed.Minutes:00}:{sw.Elapsed.Seconds:00}.{sw.ElapsedMilliseconds:000}");
return;

long GCF(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

long LCM(long a, long b)
{
    return a / GCF(a, b) * b;
}

public enum PulseType
{
    LOW,
    HIGH
}

class CommunicationMachine
{
    private List<IModule> _modules = [];
    private readonly Dictionary<PulseType, long> _pulseCounter = new() { { PulseType.LOW, 0 }, { PulseType.HIGH, 0 }};
    private readonly Queue<Pulse> _pulseQueue = new();

    public Dictionary<string, int> Part2Counters = new();

    public void Start()
    {
        var buttonModule = new ButtonModule(this, "button", [ "broadcaster" ]);
        buttonModule.Initialize();

        var rxParent = (ConjunctionModule)_modules.Where(x => x.NextModules.Any(y => y.ModuleCode == "rx")).First();

        var i=0;
        do
        {
            buttonModule.SendSignal(PulseType.LOW, "button");

            while (_pulseQueue.Count != 0)
            {
                var pulse = _pulseQueue.Dequeue();

            if (pulse.Module.ModuleCode == "rx" && Part2Counters.Count != rxParent.ModuleStates.Count)
            {
                foreach (var state in rxParent.ModuleStates)
                {
                    if (state.Value == PulseType.HIGH && !Part2Counters.ContainsKey(state.Key))
                        Part2Counters[state.Key] = i+1;
                }
            }

                pulse.Module.SendSignal(pulse.PulseType, pulse.FromModule);
                if (i < 1000)
                    _pulseCounter[pulse.PulseType]++;
            }
        } while (!(i++ >= 1000 && Part2Counters.Count == rxParent.ModuleStates.Count));
    }

    public void RegisterModule(string module, string[] nextModules)
    {
        if (module == "broadcaster")
        {
            _modules.Add(new BroadcasterModule(this, module, nextModules));
        }
        else if (module.StartsWith("%"))
        {
            _modules.Add(new FlipFlopModule(this, module.TrimStart('%'), nextModules));
        }
        else if (module.StartsWith("&"))
        {
            _modules.Add(new ConjunctionModule(this, module.TrimStart('&'), nextModules));
        }
    }

    public void InitializeModules()
    {
        _modules.ToList().ForEach(m => m.Initialize());
        _modules.ForEach(m => {
            if (m is ConjunctionModule)
                ((ConjunctionModule)m).SetupDefaultModuleStates();
        });
    }
    
    public void AddPulse(Pulse pulse)
    {
        _pulseQueue.Enqueue(pulse);
    } 

    public string[] GetConjunctionModuleInputs(string conjunctionModuleCode) =>
        _modules
            .Where(x => x.NextModules.Any(y => y.ModuleCode == conjunctionModuleCode))
            .Select(x => x.ModuleCode).ToArray();

    public IModule GetModule(string moduleCode)
    {
        var module = _modules.FirstOrDefault(x => x.ModuleCode == moduleCode);
        if (module is null)
        {
            module = new OutputModule(this, moduleCode, []);
            module.Initialize();
            _modules.Add(module);
        }
        return module;
    }

    public long GetTotalPulseValue() => _pulseCounter[PulseType.LOW] * _pulseCounter[PulseType.HIGH];
}

class Pulse(string fromModule, PulseType pulseType, IModule module)
{
    public string FromModule { get; } = fromModule;
    public PulseType PulseType { get; } = pulseType;
    public IModule Module { get; } = module;
}

interface IModule
{
    List<IModule> NextModules { get; }
    string ModuleCode { get; set; }
    void Initialize();
    void SendSignal(PulseType pulseType, string fromModule);
}

abstract class BaseModule(CommunicationMachine communicationMachine, string moduleCode, string[] nextModules) : IModule
{
    protected readonly CommunicationMachine _communicationMachine = communicationMachine;
    public List<IModule> NextModules { get; } = new();
    public string ModuleCode { get; set; } = moduleCode;

    public void Initialize() => nextModules.ToList()
        .ForEach(moduleCode => NextModules.Add(communicationMachine.GetModule(moduleCode)));

    public abstract void SendSignal(PulseType pulseType, string fromModule);
}

class ButtonModule(CommunicationMachine communicationMachine, string moduleCode, string[] nextModules) 
    : BaseModule(communicationMachine, moduleCode, nextModules) 
{
    public override void SendSignal(PulseType pulseType, string fromModule)
    {
        NextModules.ForEach(m => 
            _communicationMachine.AddPulse(new Pulse(ModuleCode, pulseType, m)));
    }
}

class BroadcasterModule(CommunicationMachine communicationMachine, string moduleCode, string[] nextModules)
    : BaseModule(communicationMachine, moduleCode, nextModules) 
{
    public override void SendSignal(PulseType pulseType, string fromModule)
    {
        NextModules.ForEach(m => 
            _communicationMachine.AddPulse(new Pulse(ModuleCode, pulseType, m)));
    }
}

class OutputModule(CommunicationMachine communicationMachine, string moduleCode, string[] nextModules)
    : BaseModule(communicationMachine, moduleCode, nextModules) 
{
    public override void SendSignal(PulseType pulseType, string fromModule)
    {
    }
}
// Prefix %
class FlipFlopModule(CommunicationMachine communicationMachine, string moduleCode, string[] nextModules)
    : BaseModule(communicationMachine, moduleCode, nextModules) 
{
    private bool _state = false;

    public override void SendSignal(PulseType pulseType, string fromModule)
    {
        if (pulseType == PulseType.HIGH)
            return;

        _state = !_state;

        NextModules.ForEach(m =>
            _communicationMachine.AddPulse(new Pulse(
                ModuleCode, 
                _state ? PulseType.HIGH : PulseType.LOW, 
                m)));
    }
}

// Prefix &
class ConjunctionModule(CommunicationMachine communicationMachine, string moduleCode, string[] nextModules)
    : BaseModule(communicationMachine, moduleCode, nextModules) 
{
    public Dictionary<string, PulseType> ModuleStates { get; } = new();

    public void SetupDefaultModuleStates()
    {
        var inputModules = _communicationMachine.GetConjunctionModuleInputs(ModuleCode);
        foreach (var inputModule in inputModules)
        {
            ModuleStates.Add(inputModule, PulseType.LOW);
        }
    }

    public override void SendSignal(PulseType pulseType, string fromModule)
    {
        ModuleStates[fromModule] = pulseType;

        NextModules.ForEach(m =>
            _communicationMachine.AddPulse(new Pulse(
                ModuleCode,
                ModuleStates.Values.All(x => x == PulseType.HIGH) 
                    ? PulseType.LOW 
                    : PulseType.HIGH,
                m
            )));
    }
}