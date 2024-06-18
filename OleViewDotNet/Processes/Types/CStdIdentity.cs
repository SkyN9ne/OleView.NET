﻿//    This file is part of OleViewDotNet.
//    Copyright (C) James Forshaw 2014, 2017
//
//    OleViewDotNet is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    OleViewDotNet is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with OleViewDotNet.  If not, see <http://www.gnu.org/licenses/>.

using NtApiDotNet;
using System;
using System.Runtime.InteropServices;

namespace OleViewDotNet.Processes.Types;

[StructLayout(LayoutKind.Sequential)]
internal struct CStdIdentity : IStdIdentity
{
    public IntPtr VTablePtr;
    public IntPtr VTablePtr2;
    public SMFLAGS _dwFlags;
    public int _cIPIDs;
    public IntPtr _pFirstIPID; // tagIPIDEntry* 

    IPIDEntryNativeInterface IStdIdentity.GetFirstIpid(NtProcess process)
    {
        if (_pFirstIPID == IntPtr.Zero)
            return null;
        return process.ReadStruct<IPIDEntryNative>(_pFirstIPID.ToInt64());
    }

    int IStdIdentity.GetIPIDCount()
    {
        return _cIPIDs;
    }

    SMFLAGS IStdIdentity.GetFlags()
    {
        return _dwFlags;
    }
}
