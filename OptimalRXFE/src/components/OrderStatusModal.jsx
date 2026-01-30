import React, { useState } from "react";

export default function OrderStatusModal({
  isOpen,
  onClose,
  order,
  stages = [],
  delivery = [],
  searchValue,
  setSearchValue,
  onSearch
}) {
  if (!isOpen) return null;

  const handleSearch = () => {
    if (!searchValue.trim()) return;
    onSearch(searchValue.trim());
  };



const trayColors = {
  BLUE: "#3B82F6",
  GRAY: "#6B7280",
  GREEN: "#22C55E",
  ORANGE: "#F97316",
  RED: "#EF4444",
  YELLOW: "#EAB308",
  NULL: "#9CA3AF"
};


  return (
<div className="fixed inset-0 bg-black/40 z-50 flex items-center justify-center">
  <div className="bg-gray-100 w-[60%] max-w-2xl rounded shadow-xl overflow-y-auto max-h-[85vh] text-sm">

    {/* HEADER */}
    <div className="flex justify-between items-center bg-white px-3 py-2 border-b">
      <h2 className="font-bold text-md">Find Order Position</h2>
      <button
        onClick={onClose}
        className="text-lg font-bold text-red-500 hover:text-red-700"
      >
        ✕
      </button>
    </div>

    {/* ORDER FOUND */}
    {order && (
      <>
        {/* ORDER HEADER INFO */}
        <div className="p-3 grid grid-cols-4 gap-2 bg-white border-b text-sm">
          <div>
            <span className="font-semibold text-xs">General Order No</span>
            <input
              type="text"
              value={searchValue}
              onChange={(e) => setSearchValue(e.target.value)}
              onKeyDown={(e) => e.key === "Enter" && handleSearch()}
              className="border px-2 py-1 w-full text-sm"
            />
          </div>

          <div>
            <span className="font-semibold text-red-600 text-xs">Track Number</span>
            <div className="border px-2 py-1 text-red-600 bg-gray-50 text-sm">
              {order.trackNo}
            </div>
          </div>

          <div>
            <span className="font-semibold text-xs">PO Number</span>
            <div className="border px-2 py-1 bg-gray-50 text-sm">
              {order.poNo || "-"}
            </div>
          </div>

          <div>
            <span className="font-semibold text-xs">Tray No</span>  
            <div
              className="border px-2 py-1 rounded text-white font-semibold text-center text-sm"
              style={{
                backgroundColor: trayColors[order.trayColor] || trayColors.NULL
              }}
            >
              {order.trayNo || "-"}
            </div>
          </div>

          {/* Status side by side */}
          <div className="col-span-3 flex items-center gap-2 mt-1">
            <span className="font-semibold text-xs">Status</span>
            <span className="px-2 py-0.5 rounded-full bg-red-100 text-red-700 font-bold text-sm">
              {order.statusText}
            </span>
          </div>
          <div className="col-span-1 flex items-center gap-2 mt-1">
            {/* Comment Button */}
            <button
              onClick={() => setCommentModalOpen(true)} // 👈 state to open the comment modal
              className="px-2 py-0.5 bg-blue-600 text-white text-xs rounded hover:bg-blue-700"
            >
              Comment
            </button>
          </div>
          
          
          
        </div>

        {/* PRODUCTION DETAILS */}
        <div className="p-3">
          <h3 className="font-bold mb-1 text-sm">Production Details</h3>
          <div className="border rounded overflow-hidden">
            <div className="overflow-y-auto" style={{ maxHeight: "4.5rem" }}>
              <table className="w-full text-sm border-collapse">
                <thead className="bg-gray-300 sticky top-0 z-10">
                  <tr>
                    <th className="border px-2 py-1 text-left text-xs">Date & Time</th>
                    <th className="border px-2 py-1 text-left text-xs">Stage</th>
                  </tr>
                </thead>
                <tbody>
                  {stages.map((s, i) => (
                    <tr key={i} className="bg-white">
                      <td className="border px-2 py-1 text-xs">{s.date} {s.time}</td>
                      <td className="border px-2 py-1 text-xs">{s.stageName}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        </div>

        {/* DELIVERY DETAILS */}
        {delivery.length > 0 && (<div className="p-3">
          <h3 className="font-bold mb-1 text-sm">Delivery Details</h3>
          <div className="overflow-x-auto">
            <table className="w-full text-sm border-collapse">
              <thead className="bg-gray-300">
                <tr>
                  <th className="border px-2 py-1 text-xs">Date</th>
                  <th className="border px-2 py-1 text-xs">Challan</th>
                  <th className="border px-2 py-1 text-xs">Mode</th>
                  <th className="border px-2 py-1 text-xs">Name</th>
                  <th className="border px-2 py-1 text-xs">Contact</th>
                </tr>
              </thead>
              <tbody>
                {delivery.length === 0 ? (
                  <tr>
                    <td colSpan="5" className="text-center py-1 bg-white text-xs">
                      No delivery data
                    </td>
                  </tr>
                ) : (
                  delivery.map((d, i) => (
                    <tr key={i} className="bg-white">
                      <td className="border px-2 py-1 text-xs">{d.date}</td>
                      <td className="border px-2 py-1 text-xs">{d.challanNo}</td>
                      <td className="border px-2 py-1 text-xs">{d.mode}</td>
                      <td className="border px-2 py-1 text-xs">{d.courierName}</td>
                      <td className="border px-2 py-1 text-xs">{d.contact}</td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        </div>)}
<div className="p-3">
  <h3 className="font-bold mb-1 text-sm">Order Details</h3>
  <div className="overflow-x-auto">

    {/* Right Column - Lens & Order Details */}
    <div className="space-y-4">

      {/* Lens Details Table */}
      <div>
        <h2 className="text-md font-semibold text-gray-800 mb-2">Lens Details</h2>
        <div className="overflow-x-auto">
          <table className="w-full text-sm text-left text-gray-600 border border-gray-200 rounded">
            <thead className="text-xs text-gray-700 uppercase bg-gray-100 sticky top-0">
              <tr>
                <th className="px-3 py-2">Eye</th>
                <th className="px-3 py-2">Sph</th>
                <th className="px-3 py-2">Cyl</th>
                <th className="px-3 py-2">Axis</th>
                <th className="px-3 py-2">Addn</th>
                <th className="px-3 py-2">Qty</th>
                
              </tr>
            </thead>
            <tbody>
              {order.powers.map((p, index) => (
                <tr key={index}  className={`border-b hover:bg-gray-50 ${p.process === "Y" ? "bg-green-100" : "bg-red-100"}`}>
                  <td className="px-3 py-2 font-medium">{p.eye}</td>
                  <td className="px-3 py-2">{p.sph}</td>
                  <td className="px-3 py-2">{p.cyl}</td>
                  <td className="px-3 py-2">{p.axis}</td>
                  <td className="px-3 py-2">{p.addn || "-"}</td>
                  <td className="px-3 py-2">{p.qty}</td>
                  
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
      <div className="bg-gray-50 p-2 rounded-lg border border-gray-100">
        {/* <h3 className="font-medium text-gray-700 text-sm">Order & Party Details</h3> */}

        {/* First row → 2 columns */}
        <div className="grid grid-cols-1 sm:grid-cols-16 gap-2 text-sm">

          <div className="sm:col-span-4">
            <span className="font-medium">Code:</span> {order.partyCode}
          </div>
          <div className="sm:col-span-9">
            <span className="font-medium">Name:</span> {order.partyName}
          </div>
          <div className="sm:col-span-3">
            <span className="font-medium">OrdNo:</span> {order.orderNo}
          </div>
        </div>
      </div>

      <div className="bg-gray-50 p-2 rounded-lg border border-gray-100">
        {/* <h3 className="font-medium text-gray-700 text-sm">Order & Party Details</h3> */}

        {/* First row → 2 columns */}
        {(order.registerNo|| order.partyOrderRefNo) && (<div className="grid grid-cols-1 sm:grid-cols-16 gap-2 text-sm">
          {order.registerNo && (<div className="sm:col-span-8">
            <span className="font-medium">SO/PO/Reg No.:</span> {order.registerNo}
          </div>)}
          {order.partyOrderRefNo && (
          <div className="sm:col-span-8">
            <span className="font-medium">Party Order Ref No.:</span> {order.partyOrderRefNo}
          </div>)}
        </div>)}
      </div>


      {(order.opticianName|| order.consumerName) &&(<div className="bg-gray-50 p-2 rounded-lg border border-gray-100">
        <div className="grid grid-cols-1 sm:grid-cols-16 gap-2 text-sm">
          {order.opticianName && (<div className="sm:col-span-8">
            <span className="font-medium">Optician Name:</span> {order.opticianName}
          </div>
          )}
 
           {order.consumerName && (<div className="sm:col-span-8">
            <span className="font-medium">Consumer Name:</span> {order.consumerName}
          </div>
          )}
        </div>
      </div>
    )}


      <div className="bg-gray-50 p-2 rounded-lg border border-gray-100">
        {/* Second row → 3 columns */}
        <div className="grid grid-cols-1 sm:grid-cols-16 gap-2 text-sm">

          <div className="sm:col-span-3"> 
            <span className="font-medium">Category:</span> {order.category}
          </div>
          <div className="sm:col-span-11">
            <span className="font-medium">LensType:</span> {order.lensType}
          </div>

          <div className="sm:col-span-2"> 
            <span className="font-medium">Size:</span> {order.size}
          </div>

        </div>
      </div>

      <div className="bg-gray-50 p-2 rounded-lg border border-gray-100">
        {/* <h3 className="font-medium text-gray-700 text-sm">Order & Party Details</h3> */}
        <div className="grid grid-cols-1 sm:grid-cols-15 gap-2 text-sm">
          <div className="sm:col-span-3">
            <span className="font-medium">Coat:</span> {order.coatColor}
          </div>
          {order.fitting === "Y" && (<div className="sm:col-span-3">
            <span className="font-medium">Frame:</span> {order.frameType}
          </div>
          )}
          {order.tintColor && (<div className="sm:col-span-5">
            <span className="font-medium">Tint:</span> {order.tintColor}
          </div>
          )}
        </div>
      </div>

      <div className="bg-gray-50 p-2 rounded-lg border border-gray-100">
        {/* <h3 className="font-medium text-gray-700 text-sm">Order & Party Details</h3> */}
        <div className="grid grid-cols-1 sm:grid-cols-15 gap-2 text-sm">
          <div className="sm:col-span-3">
            <span className="font-medium">Price:</span> {order.price}
          </div>
          <div className="sm:col-span-3">
            <span className="font-medium">Additional:</span> {order.additional}
          </div>
          <div className="sm:col-span-3">
            <span className="font-medium">Discount:</span> {order.discount}
          </div>
          <div className="sm:col-span-3">
            <span className="font-medium">Taxable:</span> {order.taxable}
          </div>
        </div>
      </div>


      {/* <div className="bg-gray-50 p-2 rounded-lg border border-gray-100">
        <div className="grid grid-cols-1 sm:grid-cols-15 gap-2 text-sm">
          <div className="sm:col-span-15">
            <span className="font-medium">Customization :</span>{" "}
            {
              [
                order.isOnlyCoating && "Coating Only",
                order.isOnlySurface && "Surface Only",
                order.isOnlyTint && "Tint Only",
                order.isOnlyFitting && "Fitting Only"
              ].filter(Boolean).join(" + ") 
            }
          </div>
        </div>
      </div> */}



            {[
        order.isOnlyCoating,
        order.isOnlySurface,
        order.isOnlyTint,
        order.isOnlyFitting
      ].some(Boolean) && (
        <div className="bg-gray-50 p-2 rounded-lg border border-gray-100">
          <div className="grid grid-cols-1 sm:grid-cols-15 gap-2 text-sm">
            <div className="sm:col-span-15">
              <span className="font-medium">Customization :</span>{" "}
              {[
                order.isOnlyCoating && "Coating Only",
                order.isOnlySurface && "Surface Only",
                order.isOnlyTint && "Tint Only",
                order.isOnlyFitting && "Fitting Only"
              ]
                .filter(Boolean)
                .join(" + ")}
            </div>
          </div>
        </div>
      )}


        {order.remarks && (<div className="bg-gray-50 p-2 rounded-lg border border-gray-100">
        {/* Remarks full width */}

          <div
            className="text-sm truncate"
            title={order.remarks}
          >
            <span className="font-medium">Remarks:</span> {order.remarks}
          </div>
        
      </div>)}

    </div>
  </div>
</div>
      </>
    )}
  </div>
</div>
  );
}
