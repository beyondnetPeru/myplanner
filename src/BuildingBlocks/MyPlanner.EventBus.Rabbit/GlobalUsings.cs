﻿global using System.Net.Sockets;
global using System.Text;
global using System.Text.Json;
global using Microsoft.Extensions.Logging;
global using Polly;
global using RabbitMQ.Client;
global using RabbitMQ.Client.Events;
global using RabbitMQ.Client.Exceptions;
global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Options;
global using MyPlanner.EventBus.Abstractions;
global using MyPlanner.EventBus.Events;
global using OpenTelemetry;
global using OpenTelemetry.Context.Propagation;
global using Polly.Retry;