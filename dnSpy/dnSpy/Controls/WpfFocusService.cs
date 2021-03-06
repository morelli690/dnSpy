﻿/*
    Copyright (C) 2014-2018 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using dnSpy.Contracts.Controls;

namespace dnSpy.Controls {
	[Export(typeof(IWpfFocusService))]
	sealed class WpfFocusService : IWpfFocusService {
		readonly Lazy<IWpfFocusChecker>[] checkers;

		public bool CanFocus => checkers.All(a => a.Value.CanFocus);

		[ImportingConstructor]
		WpfFocusService([ImportMany] IEnumerable<Lazy<IWpfFocusChecker>> checkers) => this.checkers = checkers.ToArray();

		public void Focus(IInputElement element) {
			Debug.Assert(element != null && element.Focusable);
			if (element == null || !element.Focusable)
				return;
			if (CanFocus)
				element.Focus();
		}
	}
}
