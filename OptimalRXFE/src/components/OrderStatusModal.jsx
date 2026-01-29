import React, { useState } from "react";

export default function OrderStatusModal({
  isOpen,
  onClose,
  order,
  stages = [],
  delivery = [],
  onSearch
}) {
  const [searchValue, setSearchValue] = useState("");

  if (!isOpen) return null;

  const handleSearch = () => {
    if (!searchValue.trim()) return;
    onSearch(searchValue.trim());
  };

  return (
    <div className="fixed inset-0 bg-black/40 z-50 flex items-center justify-center">
      <div className="bg-gray-100 w-[90%] max-w-6xl rounded shadow-xl overflow-y-auto max-h-[90vh]">

        {/* HEADER */}
        <div className="flex justify-between items-center bg-white px-4 py-2 border-b">
          <h2 className="font-bold text-lg">Find Order Position</h2>
          <button
            onClick={onClose}
            className="text-xl font-bold text-red-500 hover:text-red-700"
          >
            ✕
          </button>
        </div>

        {/* 🔍 SEARCH BAR */}
        <div className="p-4 bg-white border-b flex gap-2">
          <input
            type="text"
            placeholder="Enter General Order Number..."
            value={searchValue}
            onChange={(e) => setSearchValue(e.target.value)}
            onKeyDown={(e) => e.key === "Enter" && handleSearch()}
            className="border px-3 py-2 rounded w-full"
          />
          <button
            onClick={handleSearch}
            className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
          >
            Search
          </button>
        </div>

        {/* ❗ No order yet */}
        {!order && (
          <div className="p-6 text-center text-gray-600">
            Please search by General Order Number
          </div>
        )}

        {/* ✅ ORDER FOUND */}
        {order && (
          <>
            {/* ORDER HEADER INFO */}
            <div className="p-4 grid grid-cols-4 gap-4 bg-white border-b">
              <div>
                <span className="font-semibold">General Order No</span>
                <div className="border px-2 py-1 bg-gray-50">{order.gOrderNo}</div>
              </div>

              <div>
                <span className="font-semibold text-red-600">Track Number</span>
                <div className="border px-2 py-1 text-red-600 bg-gray-50">
                  {order.trackNo}
                </div>
              </div>

              <div>
                <span className="font-semibold">PO Number</span>
                <div className="border px-2 py-1 bg-gray-50">
                  {order.poNo || "-"}
                </div>
              </div>

              <div>
                <span className="font-semibold">Status</span>
                <div className="text-red-600 font-bold">
                  {order.statusText}
                </div>
              </div>
            </div>

            {/* PRODUCTION DETAILS */}
            <div className="p-4">
              <h3 className="font-bold mb-2">Production Details</h3>

              <table className="w-full text-sm border">
                <thead className="bg-gray-300">
                  <tr>
                    <th className="border px-2 py-1">Date & Time</th>
                    <th className="border px-2 py-1">Stage</th>
                  </tr>
                </thead>
                <tbody>
                  {stages.map((s, i) => (
                    <tr key={i} className="bg-white">
                      <td className="border px-2 py-1">{s.date} {s.time}</td>
                      <td className="border px-2 py-1">{s.stageName}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            {/* DELIVERY DETAILS */}
            <div className="p-4">
              <h3 className="font-bold mb-2">Delivery Detail</h3>

              <table className="w-full text-sm border">
                <thead className="bg-gray-300">
                  <tr>
                    <th className="border px-2 py-1">Date</th>
                    <th className="border px-2 py-1">Challan</th>
                    <th className="border px-2 py-1">Mode</th>
                    <th className="border px-2 py-1">AWB No</th>
                    <th className="border px-2 py-1">Contact</th>
                  </tr>
                </thead>
                <tbody>
                  {delivery.length === 0 ? (
                    <tr>
                      <td colSpan="5" className="text-center py-2 bg-white">
                        No delivery data
                      </td>
                    </tr>
                  ) : (
                    delivery.map((d, i) => (
                      <tr key={i} className="bg-white">
                        <td className="border px-2 py-1">{d.date}</td>
                        <td className="border px-2 py-1">{d.challanNo}</td>
                        <td className="border px-2 py-1">{d.mode}</td>
                        <td className="border px-2 py-1">{d.awbNo}</td>
                        <td className="border px-2 py-1">{d.contact}</td>
                      </tr>
                    ))
                  )}
                </tbody>
              </table>
            </div>
          </>
        )}
      </div>
    </div>
  );
}
