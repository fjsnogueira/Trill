﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Microsoft.StreamProcessing
{
    using System.Linq;
    using System.Reflection;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    internal partial class FixedIntervalEquiJoinTemplate : CommonBaseTemplate
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(@"// *********************************************************************
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License
// *********************************************************************
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.StreamProcessing;
using Microsoft.StreamProcessing.Aggregates;
using Microsoft.StreamProcessing.Internal;
using Microsoft.StreamProcessing.Internal.Collections;
[assembly: IgnoresAccessChecksTo(""Microsoft.StreamProcessing"")]

// TKey: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write("\r\n// TLeft: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TLeft));
            this.Write("\r\n// TRight: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TRight));
            this.Write("\r\n// TResult: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write("\r\n\r\n[DataContract]\r\ninternal sealed class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write(this.ToStringHelper.ToStringWithCulture(genericParameters));
            this.Write(" : BinaryPipe<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TLeft));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TRight));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write(">\r\n{\r\n    private readonly MemoryPool<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write("> pool;\r\n\r\n    [SchemaSerialization]\r\n    private readonly Expression<Func<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", bool>> keyComparer;\r\n    private readonly Func<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", bool> keyComparerEquals;\r\n\r\n    private StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write("> genericOutputBatch;\r\n    [DataMember]\r\n    private ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TResult));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTResultGenericParameters));
            this.Write(@" output;
    [DataMember]
    private FastMap<ActiveIntervalLeft> leftIntervalMap = new FastMap<ActiveIntervalLeft>();
    [DataMember]
    private FastMap<ActiveIntervalRight> rightIntervalMap = new FastMap<ActiveIntervalRight>();
    [DataMember]
    private long nextLeftTime = long.MinValue;
    [DataMember]
    private long nextRightTime = long.MinValue;
    [DataMember]
    private long currTime = long.MinValue;

    private readonly Func<PlanNode, PlanNode, IBinaryObserver, BinaryPlanNode> queryPlanGenerator;

    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(staticCtor));
            this.Write("\r\n\r\n    [Obsolete(\"Used only by serialization. Do not call directly.\")]\r\n    publ" +
                    "ic ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("() { }\r\n\r\n    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("(\r\n        IStreamable<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write("> stream,\r\n        IStreamObserver<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write(@"> observer,
        Func<PlanNode, PlanNode, IBinaryObserver, BinaryPlanNode> queryPlanGenerator)
        : base(stream, observer)
    {
        this.queryPlanGenerator = queryPlanGenerator;

        keyComparer = stream.Properties.KeyEqualityComparer.GetEqualsExpr();
        keyComparerEquals = keyComparer.Compile();

        pool = MemoryManager.GetMemoryPool<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write(@">(true /*stream.Properties.IsColumnar*/);
        GetOutputBatch();
    }

    public override int CurrentlyBufferedOutputCount => output.Count;
    public override int CurrentlyBufferedLeftInputCount => base.CurrentlyBufferedLeftInputCount + leftIntervalMap.Count;
    public override int CurrentlyBufferedRightInputCount => base.CurrentlyBufferedRightInputCount + rightIntervalMap.Count;

    protected override void ProduceBinaryQueryPlan(PlanNode left, PlanNode right)
    {
        Observer.ProduceQueryPlan(queryPlanGenerator(left, right, this));
    }

    protected override void DisposeState() => this.output.Free();

    private void GetOutputBatch()
    {
        pool.Get(out genericOutputBatch);
        genericOutputBatch.Allocate();
        output = (");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TResult));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTResultGenericParameters));
            this.Write(")genericOutputBatch;\r\n");
 foreach (var f in this.resultFields.Where(fld => fld.OptimizeString())) {  
            this.Write("\r\n        output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".Initialize();\r\n");
 } 
            this.Write("    }\r\n\r\n    [MethodImpl(MethodImplOptions.AggressiveInlining)]\r\n    protected ov" +
                    "erride void ProcessBothBatches(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TLeft));
            this.Write("> genericLeftBatch, StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TRight));
            this.Write("> genericRightBatch, out bool leftBatchDone, out bool rightBatchDone, out bool le" +
                    "ftBatchFree, out bool rightBatchFree)\r\n    {\r\n        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(LeftBatchType));
            this.Write(" leftBatch = genericLeftBatch as ");
            this.Write(this.ToStringHelper.ToStringWithCulture(LeftBatchType));
            this.Write(";\r\n        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(RightBatchType));
            this.Write(" rightBatch = genericRightBatch as ");
            this.Write(this.ToStringHelper.ToStringWithCulture(RightBatchType));
            this.Write(";\r\n        leftBatchFree = rightBatchFree = true;\r\n\r\n        if (!GoToVisibleRow(" +
                    "genericLeftBatch))\r\n        {\r\n            leftBatchDone = true;\r\n            ri" +
                    "ghtBatchDone = false;\r\n            return;\r\n        }\r\n\r\n        this.nextLeftTi" +
                    "me = genericLeftBatch.vsync.col[genericLeftBatch.iter];\r\n\r\n        if (!GoToVisi" +
                    "bleRow(genericRightBatch))\r\n        {\r\n            leftBatchDone = false;\r\n     " +
                    "       rightBatchDone = true;\r\n            return;\r\n        }\r\n\r\n        this.ne" +
                    "xtRightTime = genericRightBatch.vsync.col[genericRightBatch.iter];\r\n\r\n        wh" +
                    "ile (true)\r\n        {\r\n            if (nextLeftTime <= nextRightTime)\r\n         " +
                    "   {\r\n                UpdateTime(nextLeftTime);\r\n                ProcessLeftEven" +
                    "t(\r\n                    nextLeftTime,\r\n                    leftBatch.vother.col[" +
                    "leftBatch.iter],\r\n                    ref leftBatch.key.col[leftBatch.iter],\r\n  " +
                    "                  leftBatch,\r\n                    leftBatch.iter,\r\n             " +
                    "       leftBatch.hash.col[leftBatch.iter]);\r\n\r\n                leftBatch.iter++;" +
                    "\r\n\r\n                if (!GoToVisibleRow(leftBatch))\r\n                {\r\n        " +
                    "            leftBatchDone = true;\r\n                    rightBatchDone = false;\r\n" +
                    "                    break;\r\n                }\r\n\r\n                this.nextLeftTi" +
                    "me = leftBatch.vsync.col[leftBatch.iter];\r\n            }\r\n            else\r\n    " +
                    "        {\r\n                UpdateTime(nextRightTime);\r\n                ProcessRi" +
                    "ghtEvent(\r\n                    nextRightTime,\r\n                    rightBatch.vo" +
                    "ther.col[rightBatch.iter],\r\n                    ref rightBatch.key.col[rightBatc" +
                    "h.iter],\r\n                    rightBatch,\r\n                    rightBatch.iter,\r" +
                    "\n                    rightBatch.hash.col[rightBatch.iter]);\r\n\r\n                r" +
                    "ightBatch.iter++;\r\n\r\n                if (!GoToVisibleRow(rightBatch))\r\n         " +
                    "       {\r\n                    leftBatchDone = false;\r\n                    rightB" +
                    "atchDone = true;\r\n                    break;\r\n                }\r\n\r\n             " +
                    "   this.nextRightTime = rightBatch.vsync.col[rightBatch.iter];\r\n            }\r\n " +
                    "       }\r\n\r\n        return;\r\n    }\r\n\r\n    [MethodImpl(MethodImplOptions.Aggressi" +
                    "veInlining)]\r\n    protected override void ProcessLeftBatch(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TLeft));
            this.Write("> genericLeftBatch, out bool isBatchDone, out bool isBatchFree)\r\n    {\r\n        i" +
                    "sBatchFree = true;\r\n        var batch = (");
            this.Write(this.ToStringHelper.ToStringWithCulture(LeftBatchType));
            this.Write(@") genericLeftBatch;
        while (true)
        {
            if (!GoToVisibleRow(batch))
            {
                isBatchDone = true;
                break;
            }

            this.nextLeftTime = batch.vsync.col[batch.iter];

            if (nextLeftTime > nextRightTime)
            {
                isBatchDone = false;
                break;
            }

            UpdateTime(nextLeftTime);

            ProcessLeftEvent(
                nextLeftTime,
                batch.vother.col[batch.iter],
                ref batch.key.col[batch.iter],
                batch,
                batch.iter,
                batch.hash.col[batch.iter]);

            batch.iter++;
        }

        return;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void ProcessRightBatch(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TRight));
            this.Write("> genericRightBatch, out bool isBatchDone, out bool isBatchFree)\r\n    {\r\n        " +
                    "isBatchFree = true;\r\n        var batch = (");
            this.Write(this.ToStringHelper.ToStringWithCulture(RightBatchType));
            this.Write(@") genericRightBatch;

        while (true)
        {
            if (!GoToVisibleRow(batch))
            {
                isBatchDone = true;
                break;
            }

            this.nextRightTime = batch.vsync.col[batch.iter];

            if (nextRightTime > nextLeftTime)
            {
                isBatchDone = false;
                break;
            }

            UpdateTime(nextRightTime);

            ProcessRightEvent(
                nextRightTime,
                batch.vother.col[batch.iter],
                ref batch.key.col[batch.iter],
                batch,
                batch.iter,
                batch.hash.col[batch.iter]);

            batch.iter++;
        }

        return;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool GoToVisibleRow<TPayload>(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(@", TPayload> batch)
    {
        while (batch.iter < batch.Count && (batch.bitvector.col[batch.iter >> 6] & (1L << (batch.iter & 0x3f))) != 0 && batch.vother.col[batch.iter] >= 0)
        {
            batch.iter++;
        }

        return batch.iter != batch.Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void UpdateTime(long time)
    {
        if (time != currTime)
        {
            LeaveTime();
            currTime = time;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ProcessLeftEvent(long start, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(LeftBatchType));
            this.Write(@" leftBatch, int leftIndex, int hash)
    {
        if (start < end)
        {
            // Row is an interval.
            if (nextRightTime > start)
            {
                int index = leftIntervalMap.Insert(hash);
                leftIntervalMap.Values[index].Populate(start, end, ref key, leftBatch, leftIndex);
                CreateOutputForLeftStartInterval(start, end, ref key, leftBatch, leftIndex, hash);
            }
            else
            {
                int index = leftIntervalMap.InsertInvisible(hash);
                leftIntervalMap.Values[index].Populate(start, end, ref key, leftBatch, leftIndex);
            }
        }
        else if (end == StreamEvent.PunctuationOtherTime)
        {
            AddPunctuationToBatch(start);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ProcessRightEvent(long start, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(RightBatchType));
            this.Write(" rightBatch, int rightRowIndex, int hash)\r\n    {\r\n        if (start < end)\r\n     " +
                    "   {\r\n            // Row is an interval.\r\n            if (nextLeftTime > start)\r" +
                    "\n            {\r\n                int index = rightIntervalMap.Insert(hash);\r\n    " +
                    "            rightIntervalMap.Values[index].Populate(start, end, ref key, rightBa" +
                    "tch, rightRowIndex);\r\n                CreateOutputForRightStartInterval(start, e" +
                    "nd, ref key, rightBatch, rightRowIndex, hash);\r\n            }\r\n            else\r" +
                    "\n            {\r\n                int index = rightIntervalMap.InsertInvisible(has" +
                    "h);\r\n                rightIntervalMap.Values[index].Populate(start, end, ref key" +
                    ", rightBatch, rightRowIndex);\r\n            }\r\n        }\r\n        else if (end ==" +
                    " StreamEvent.PunctuationOtherTime)\r\n        {\r\n            AddPunctuationToBatch" +
                    "(start);\r\n        }\r\n    }\r\n\r\n    [MethodImpl(MethodImplOptions.AggressiveInlini" +
                    "ng)]\r\n    private void LeaveTime()\r\n    {\r\n        int index;\r\n        int hash;" +
                    "\r\n        var leftIntervals = leftIntervalMap.TraverseInvisible();\r\n        whil" +
                    "e (leftIntervals.Next(out index, out hash))\r\n        {\r\n            long end = l" +
                    "eftIntervalMap.Values[index].End;\r\n            CreateOutputForLeftStartInterval(" +
                    "\r\n                currTime,\r\n                end,\r\n                ref leftInter" +
                    "valMap.Values[index].Key,\r\n                ref leftIntervalMap.Values[index],\r\n " +
                    "               hash);\r\n            leftIntervals.MakeVisible();\r\n        }\r\n\r\n  " +
                    "      var rightIntervals = rightIntervalMap.TraverseInvisible();\r\n        while " +
                    "(rightIntervals.Next(out index, out hash))\r\n        {\r\n            long end = ri" +
                    "ghtIntervalMap.Values[index].End;\r\n            CreateOutputForRightStartInterval" +
                    "(\r\n                currTime,\r\n                end,\r\n                ref rightInt" +
                    "ervalMap.Values[index].Key,\r\n                ref rightIntervalMap.Values[index]," +
                    "\r\n                hash);\r\n            rightIntervals.MakeVisible();\r\n        }\r\n" +
                    "    }\r\n\r\n    [MethodImpl(MethodImplOptions.AggressiveInlining)]\r\n    private voi" +
                    "d CreateOutputForLeftStartInterval(long currentTime, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(LeftBatchType));
            this.Write(" leftBatch, int leftIndex, int hash)\r\n    {\r\n        // Create end edges for all " +
                    "joined right intervals.\r\n        var intervals = rightIntervalMap.Find(hash);\r\n " +
                    "       while (intervals.Next(out var index))\r\n        {\r\n            if (");
            this.Write(this.ToStringHelper.ToStringWithCulture(keyComparerEquals("key", "rightIntervalMap.Values[index].Key")));
            this.Write(@")
            {
                long rightEnd = rightIntervalMap.Values[index].End;
                AddToBatch(
                    currentTime,
                    end < rightEnd ? end : rightEnd,
                    ref key,
                    leftBatch, leftIndex,
                    ref rightIntervalMap.Values[index],
                    hash);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CreateOutputForLeftStartInterval(long currentTime, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ref ActiveIntervalLeft leftInterval, int hash)\r\n    {\r\n        // Create en" +
                    "d edges for all joined right intervals.\r\n        var intervals = rightIntervalMa" +
                    "p.Find(hash);\r\n        while (intervals.Next(out var index))\r\n        {\r\n       " +
                    "     if (");
            this.Write(this.ToStringHelper.ToStringWithCulture(keyComparerEquals("key", "rightIntervalMap.Values[index].Key")));
            this.Write(@")
            {
                long rightEnd = rightIntervalMap.Values[index].End;
                AddToBatch(
                    currentTime,
                    end < rightEnd ? end : rightEnd,
                    ref key,
                    ref leftInterval,
                    ref rightIntervalMap.Values[index],
                    hash);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CreateOutputForRightStartInterval(long currentTime, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(RightBatchType));
            this.Write(" rightBatch, int rightIndex, int hash)\r\n    {\r\n        // Create end edges for al" +
                    "l joined left intervals.\r\n        var intervals = leftIntervalMap.Find(hash);\r\n " +
                    "       while (intervals.Next(out var index))\r\n        {\r\n            if (");
            this.Write(this.ToStringHelper.ToStringWithCulture(keyComparerEquals("key", "leftIntervalMap.Values[index].Key")));
            this.Write(@")
            {
                long leftEnd = leftIntervalMap.Values[index].End;
                AddToBatch(
                    currentTime,
                    end < leftEnd ? end : leftEnd,
                    ref key,
                    ref leftIntervalMap.Values[index],
                    rightBatch, rightIndex,
                    hash);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CreateOutputForRightStartInterval(long currentTime, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ref ActiveIntervalRight rightInterval, int hash)\r\n    {\r\n        // Create " +
                    "end edges for all joined left intervals.\r\n        var intervals = leftIntervalMa" +
                    "p.Find(hash);\r\n        while (intervals.Next(out var index))\r\n        {\r\n       " +
                    "     if (");
            this.Write(this.ToStringHelper.ToStringWithCulture(keyComparerEquals("key", "leftIntervalMap.Values[index].Key")));
            this.Write(@")
            {
                long leftEnd = leftIntervalMap.Values[index].End;
                AddToBatch(
                    currentTime,
                    end < leftEnd ? end : leftEnd,
                    ref key,
                    ref leftIntervalMap.Values[index],
                    ref rightInterval,
                    hash);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AddPunctuationToBatch(long start)
    {
        if (start > lastCTI)
        {
            lastCTI = start;

            int index = output.Count++;
            output.vsync.col[index] = start;
            output.vother.col[index] = StreamEvent.PunctuationOtherTime;
            output.key.col[index] = default;
            output[index] = default;
            output.hash.col[index] = 0;
            output.bitvector.col[index >> 6] |= (1L << (index & 0x3f));

            if (output.Count == Config.DataBatchSize) FlushContents();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AddToBatch(long start, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ref ActiveIntervalLeft leftInterval, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(RightBatchType));
            this.Write(@" rightBatch, int rightIndex, int hash)
    {
        int index = output.Count++;
        output.vsync.col[index] = start;
        output.vother.col[index] = end;
        output.key.col[index] = key;
        //output[index] = selector(leftPayload, rightPayload);
        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(rightBatchSelector("leftInterval.Payload", "rightBatch", "rightIndex")));
            this.Write("\r\n        output.hash.col[index] = hash;\r\n\r\n        if (output.Count == Config.Da" +
                    "taBatchSize) FlushContents();\r\n    }\r\n\r\n    [MethodImpl(MethodImplOptions.Aggres" +
                    "siveInlining)]\r\n    private void AddToBatch(long start, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(LeftBatchType));
            this.Write(@" leftBatch, int leftIndex, ref ActiveIntervalRight rightInterval, int hash)
    {
        int index = output.Count++;
        output.vsync.col[index] = start;
        output.vother.col[index] = end;
        output.key.col[index] = key;
        //output[index] = selector(leftPayload, rightPayload);
        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(leftBatchSelector("leftBatch", "leftIndex", "rightInterval.Payload")));
            this.Write("\r\n        output.hash.col[index] = hash;\r\n\r\n        if (output.Count == Config.Da" +
                    "taBatchSize) FlushContents();\r\n    }\r\n\r\n    [MethodImpl(MethodImplOptions.Aggres" +
                    "siveInlining)]\r\n    private void AddToBatch(long start, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(@" key, ref ActiveIntervalLeft leftInterval, ref ActiveIntervalRight rightInterval, int hash)
    {
        int index = output.Count++;
        output.vsync.col[index] = start;
        output.vother.col[index] = end;
        output.key.col[index] = key;
        //output[index] = selector(leftPayload, rightPayload);
        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(activeSelector("leftInterval.Payload", "rightInterval.Payload")));
            this.Write(@"
        output.hash.col[index] = hash;

        if (output.Count == Config.DataBatchSize) FlushContents();
    }

    protected override void FlushContents()
    {
        if (output.Count == 0) return;
        output.Seal();
        this.Observer.OnNext(output);
        GetOutputBatch();
    }

");
 if (!this.leftType.GetTypeInfo().IsValueType) { 
            this.Write("    [DataContract]\r\n    private struct ");
            this.Write(this.ToStringHelper.ToStringWithCulture(ActiveEventTypeLeft));
            this.Write("\r\n    {\r\n        ");
 foreach (var f in this.leftFields) { 
            this.Write("        [DataMember]\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Type.GetCSharpSourceSyntax()));
            this.Write(" ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.OriginalName));
            this.Write(";\r\n        ");
 } 
            this.Write("    }\r\n");
 } 
 if (!this.rightType.GetTypeInfo().IsValueType) { 
            this.Write("    [DataContract]\r\n    private struct ");
            this.Write(this.ToStringHelper.ToStringWithCulture(ActiveEventTypeRight));
            this.Write("\r\n    {\r\n        ");
 foreach (var f in this.rightFields) { 
            this.Write("        [DataMember]\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Type.GetCSharpSourceSyntax()));
            this.Write(" ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.OriginalName));
            this.Write(";\r\n        ");
 } 
            this.Write("    }\r\n");
 } 
            this.Write("\r\n    [DataContract]\r\n    private struct ActiveIntervalLeft\r\n    {\r\n        [Data" +
                    "Member]\r\n        public long Start;\r\n        [DataMember]\r\n        public long E" +
                    "nd;\r\n        [DataMember]\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" Key;\r\n\r\n        [DataMember]\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(ActiveEventTypeLeft));
            this.Write(" Payload;\r\n\r\n        [MethodImpl(MethodImplOptions.AggressiveInlining)]\r\n        " +
                    "public void Populate(long start, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(LeftBatchType));
            this.Write(" batch, int index)\r\n        {\r\n            Start = start;\r\n            End = end;" +
                    "\r\n            Key = key;\r\n\r\n");
 if (this.leftMessageRepresentation.noFields) { 
            this.Write("            this.Payload = batch.payload.col[index];\r\n");
 } else { 
     foreach (var f in this.leftFields) { 
            this.Write("            this.Payload.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.OriginalName));
            this.Write(" = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.AccessExpressionForRowValue("batch", "index")));
            this.Write(";\r\n");
 } 
 } 
            this.Write(@"
        }

        public override string ToString()
        {
            return ""[Start="" + Start + "", End="" + End + "", Key='"" + Key + ""', Payload='"" + ""']"";
        }
    }

    [DataContract]
    private struct ActiveIntervalRight
    {
        [DataMember]
        public long Start;
        [DataMember]
        public long End;
        [DataMember]
        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" Key;\r\n\r\n        [DataMember]\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(ActiveEventTypeRight));
            this.Write(" Payload;\r\n\r\n        [MethodImpl(MethodImplOptions.AggressiveInlining)]\r\n        " +
                    "public void Populate(long start, long end, ref ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(RightBatchType));
            this.Write(" batch, int index)\r\n        {\r\n            Start = start;\r\n            End = end;" +
                    "\r\n            Key = key;\r\n\r\n");
 if (this.rightMessageRepresentation.noFields) { 
            this.Write("            this.Payload = batch.payload.col[index];\r\n");
 } else { 
     foreach (var f in this.rightFields) { 
            this.Write("            this.Payload.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.OriginalName));
            this.Write(" = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.AccessExpressionForRowValue("batch", "index")));
            this.Write(";\r\n");
 } 
 } 
            this.Write("\r\n        }\r\n\r\n        public override string ToString()\r\n        {\r\n            " +
                    "return \"[Start=\" + Start + \", End=\" + End + \", Key=\'\" + Key + \"\', Payload=\'\" + \"" +
                    "\']\";\r\n        }\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
}
