// import { useEffect, useState ,useMemo} from "react";
// import React from "react";
// import api from "../api/axios";

import { useEffect, useState, useMemo } from "react";
import api from "../api/axios";
import OrderStatusModal from "../components/OrderStatusModal";

function Orders() {
  const [activeType, setActiveType] = useState("ALL"); 
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  // const [date, setDate] = useState(new Date().toISOString().split("T")[0]);
  const [openStatus, setOpenStatus] = useState(false);
  const [selectedOrder, setSelectedOrder] = useState(null);
const [searchValue, setSearchValue] = useState("");
  const [stageData, setStageData] = useState([]);
  const [deliveryData, setDeliveryData] = useState([]);


  const today = new Date().toISOString().split("T")[0];

  const [fromDate, setFromDate] = useState(today);
  const [toDate, setToDate] = useState(today);

  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 20;

  // sorting state
  const [sortConfig, setSortConfig] = useState({
    key: null,
    direction: "asc",
  });

  // 🔹 SORT FIRST
  const sortedOrders = useMemo(() => {
    if (!sortConfig.key) return orders;

    const sorted = [...orders].sort((a, b) => {
      let aVal = a[sortConfig.key];
      let bVal = b[sortConfig.key];

      if (aVal == null) return 1;
      if (bVal == null) return -1;

      if (
        sortConfig.key === "order_Date" ||
        sortConfig.key === "receivedOnDate" ||
        sortConfig.key === "orderEntryTime"
      ) {
        aVal = new Date(aVal);
        bVal = new Date(bVal);
      }

      if (typeof aVal === "number" && typeof bVal === "number") {
        return sortConfig.direction === "asc"
          ? aVal - bVal
          : bVal - aVal;
      }

      return sortConfig.direction === "asc"
        ? String(aVal).localeCompare(String(bVal))
        : String(bVal).localeCompare(String(aVal));
    });

    return sorted;
  }, [orders, sortConfig]);

  // 🔹 NOW SAFE TO USE sortedOrders
  const totalRecords = sortedOrders.length;
  const totalPages = Math.ceil(totalRecords / pageSize);

  // 🔹 PAGINATION
  const paginatedOrders = useMemo(() => {
    const start = (currentPage - 1) * pageSize;
    return sortedOrders.slice(start, start + pageSize);
  }, [sortedOrders, currentPage]);

  // 🔹 SORT HANDLER
  const requestSort = (key) => {
    setCurrentPage(1); // reset page on sort
    setSortConfig((prev) => ({
      key,
      direction:
        prev.key === key && prev.direction === "asc" ? "desc" : "asc",
    }));
  };

  const sortIcon = (key) => {
    if (sortConfig.key !== key) return "↕";
    return sortConfig.direction === "asc" ? "↑" : "↓";
  };
// const demoOrder = {
//   gOrderNo: "GKB123456",
//   trackNo: "TRK987654",
//   poNo: "PO-456",
//   statusText: "IN PRODUCTION",
//   partyCode: "P001",
//   partyName: "Vision Optics",
//   orderNo: "ORD789",
//   lensType: "Progressive",
//   rightEye: "+1.50 / -0.50",
//   leftEye: "+1.25 / -0.25",
//   lensSize: "70",
//   remarks: "Urgent order"
// };

// const demoStages = [
//   { date: "2026-01-25", time: "10:30", stageName: "Order Entry" },
//   { date: "2026-01-25", time: "12:00", stageName: "Surfacing" },
//   { date: "2026-01-26", time: "09:15", stageName: "Coating" },
//   { date: "2026-01-26", time: "16:00", stageName: "Dispatch" }
// ];

// const demoDelivery = [
//   {
//     date: "2026-01-27",
//     challanNo: "CH123",
//     mode: "Courier",
//     awbNo: "AWB998877",
//     contact: "9876543210"
//   }
// ];


// const handleModalSearch = (gOrderNo) => {
//   // 🔹 demo data lookup (replace with API later)
//   if (gOrderNo === "GKB123456") {
//     setSelectedOrder(demoOrder);
//     setStageData(demoStages);
//     setDeliveryData(demoDelivery);
//   } else {
//     setSelectedOrder(null);
//     setStageData([]);
//     setDeliveryData([]);
//     alert("Order not found");
//   }
// };

const handleModalSearch = async (gOrderNo) => {
  try {
    setSearchValue(gOrderNo); // 👈 updates modal input
    const res = await api.get(`/orders/details/${gOrderNo}`);
    setSelectedOrder(res.data.order);
    setStageData(res.data.stages);
    setDeliveryData(res.data.delivery);
  } catch (err) {
    setSelectedOrder(null);
    setStageData([]);
    setDeliveryData([]);
    alert("Order not found");
  }
};


const ORDER_TYPE_MAP = {
  ALL: "ALL",
  CurrentPending: "CurrentPending",
  FittingOrders: "FittingOrders",
  CompletedButNotDelivered: "CompletedButNotDelivered",
  Delivered: "Delivered",
  Cancelled: "Cancelled",
  OtherLabOrders: "OtherLabOrders",
};



useEffect(() => {
  if (!fromDate || !toDate) return;

  const fetchOrders = async () => {
    setLoading(true);
    setError("");

    try {
      const res = await api.get(
        `/orders/by-date-range`,
        {
          params: {
            fromDate,
            toDate,
            type: ORDER_TYPE_MAP[activeType],
          },
        }
      );

      setOrders(res.data);
      setCurrentPage(1);
    } catch (err) {
      console.error(err);
      setError("Failed to fetch orders.");
    } finally {
      setLoading(false);
    }
  };

  fetchOrders();
}, [fromDate, toDate, activeType]);




  return (
    <div className="p-6 bg-gray-100 min-h-screen">
      <h1 className="text-2xl font-bold mb-4">Orders</h1>

    <div className="mb-4 flex gap-4 items-end">
      <div>
        <label className="block text-sm font-semibold mb-1">From Date</label>
        <input
          type="date"
          value={fromDate}
          onChange={(e) => setFromDate(e.target.value)}
          className="border px-3 py-2 rounded"
        />
      </div>

      <div>
        <label className="block text-sm font-semibold mb-1">To Date</label>
        <input
          type="date"
          value={toDate}
          onChange={(e) => setToDate(e.target.value)}
          className="border px-3 py-2 rounded"
        />
      </div>
    </div>

      {/* 🔹 Sticky Logo Selector */}
      <div className="sticky top-0 z-30 bg-gray-100 pb-3">
        <div className="flex gap-4 p-4 bg-white rounded-xl shadow border">
          {[
            {
              key: "ALL",
              label: "All Orders",
              logo: "/logo/all.svg",
            },
            {
              key: "CurrentPending",
              label: "Current Pending",
              logo: "/logo/currentpending.svg",
            },
            {
              key: "FittingOrders",
              label: "Fitting Orders",
              logo: "/logo/fittingorders.svg",
            },
            {
              key: "CompletedButNotDelivered",
              label: "Completed But Not Delivered",
              logo: "/logo/completedbutnotdelivered.svg",
            },
            {
              key: "Delivered",
              label: "Delivered",
              logo: "/logo/delivered.svg",
            },
            {
              key: "Cancelled",
              label: "Cancelled",
              logo: "/logo/cancelled.svg",
            },
            {
              key: "OtherLabOrders",
              label: "Other Lab Orders",
              logo: "/logo/otherlaborders.svg",
            },
          ].map(item => (
            <button
              key={item.key}
              // onClick={() => setActiveType(item.key)}

              onClick={() => {
                  setActiveType(item.key);
                  setCurrentPage(1);
                  setSortConfig({ key: null, direction: "asc" });
                }}
              className={`flex items-center gap-3 px-6 py-3 rounded-xl border transition-all
                ${activeType === item.key
                  ? "border-blue-600 bg-blue-50 shadow-lg scale-105 ring-2 ring-blue-400"
                  : "border-gray-200 bg-white hover:bg-gray-50"}
              `}
              
            >
              {loading && activeType === item.key && (
              <span className="text-xs text-blue-600 ml-2">Loading...</span>
            )}
              <img
                src={item.logo}
                alt={item.label}
                className="h-10 w-10 object-contain"
              />
              <span className="font-semibold text-gray-700">
                {item.label}
              </span>
            </button>
          ))}
        </div>
      </div>


      {loading ? (
        <p>Loading...</p>
      ) : error ? (
        <p className="text-red-500">{error}</p>
      ) : (
            <div className="bg-white rounded-xl shadow-lg border border-gray-200 overflow-hidden">
              <div className="overflow-x-auto">
                <table className="min-w-full text-sm text-gray-700">
                  <thead className="bg-gradient-to-r from-blue-600 to-blue-700 text-white sticky top-0 z-10">
                    <tr>
                      <th className="px-4 py-3 text-left cursor-pointer" onClick={() => requestSort("party_code")}>
                        Party Code {sortIcon("party_code")}
                      </th>
                      <th className="px-4 py-3 cursor-pointer" onClick={() => requestSort("order_No")}>
                        Ord No {sortIcon("order_No")}
                      </th>
                      <th className="px-4 py-3 cursor-pointer" onClick={() => requestSort("gOrderNo")}>
                        GOrd No {sortIcon("gOrderNo")}
                      </th>
                      <th className="px-4 py-3 cursor-pointer" onClick={() => requestSort("orderEntryTime")}>
                        Entry Time {sortIcon("orderEntryTime")}
                      </th>
                      <th className="px-4 py-3 cursor-pointer" onClick={() => requestSort("lens_type")}>
                        Lens Type {sortIcon("lens_type")}
                      </th>
                      <th className="px-4 py-3 cursor-pointer" onClick={() => requestSort("coatColor")}>
                        Coat {sortIcon("coatColor")}
                      </th>
                      <th className="px-4 py-3 cursor-pointer" onClick={() => requestSort("fitting")}>
                        Fit {sortIcon("fitting")}
                      </th>
                      <th className="px-4 py-3 cursor-pointer" onClick={() => requestSort("stockorder")}>
                        Type {sortIcon("stockorder")}
                      </th>
                      <th className="px-4 py-3 cursor-pointer" onClick={() => requestSort("currentStage")}>
                        Status {sortIcon("currentStage")}
                      </th>
                      <th className="px-4 py-3">L Order</th>
                      <th className="px-4 py-3">Reg No</th>
                      <th className="px-4 py-3">Customer</th>
                    </tr>
                  </thead>

                  <tbody>
                    {paginatedOrders.map((order, idx) => (
                      <tr
                        key={order.gOrderNo}
                        className={`border-b ${
                          idx % 2 === 0 ? "bg-gray-50" : "bg-white"
                        } hover:bg-blue-50 transition`}
                      >
                        <td className="px-4 py-2">{order.party_code}</td>
                        <td className="px-4 py-2">{order.order_No}</td>
                        <td onClick={() => {setOpenStatus(true); handleModalSearch(order.gOrderNo); }}className="cursor-pointer text-blue-600 font-semibold hover:underline">{order.gOrderNo}</td>
                        <td className="px-4 py-2">
                          {new Date(order.orderEntryTime).toLocaleString()}
                        </td>
                        <td className="px-4 py-2 max-w-xs truncate" title={order.lens_type}>
                          {order.lens_type}
                        </td>
                        <td className="px-4 py-2">{order.coatColor}</td>
                        <td className="px-4 py-2">{order.fitting}</td>
                        <td className="px-4 py-2">
                          <span
                            className={`px-2 py-1 rounded-full text-xs font-medium ${
                              order.stockorder === "Y"
                                ? "bg-green-100 text-green-700"
                                : "bg-orange-100 text-orange-700"
                            }`}
                          >
                            {order.stockorder === "Y" ? "STOCK" : "RX"}
                          </span>
                        </td>
                        <td className="px-4 py-2">{order.currentStage}</td>
                        <td className="px-4 py-2">{order.l_OrderNo}</td>
                        <td className="px-4 py-2">{order.registerno}</td>
                        <td className="px-4 py-2">{order.party_cust_code || "-"}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
                <div className="flex items-center justify-between px-4 py-3 bg-gray-50 border-t">
                  <div className="text-sm text-gray-600">
                    Showing{" "}
                    <span className="font-medium">
                      {(currentPage - 1) * pageSize + 1}
                    </span>{" "}
                    to{" "}
                    <span className="font-medium">
                      {Math.min(currentPage * pageSize, totalRecords)}
                    </span>{" "}
                    of{" "}
                    <span className="font-medium">{totalRecords}</span>{" "}
                    records
                  </div>

                  <div className="flex items-center gap-1">
                    <button
                      onClick={() => setCurrentPage(p => Math.max(p - 1, 1))}
                      disabled={currentPage === 1}
                      className="px-3 py-1 text-sm rounded-md border bg-white hover:bg-gray-100 disabled:opacity-50"
                    >
                      Prev
                    </button>

                    {Array.from({ length: totalPages }, (_, i) => i + 1)
                      .slice(
                        Math.max(0, currentPage - 3),
                        Math.min(totalPages, currentPage + 2)
                      )
                      .map(page => (
                        <button
                          key={page}
                          onClick={() => setCurrentPage(page)}
                          className={`px-3 py-1 text-sm rounded-md border ${
                            page === currentPage
                              ? "bg-blue-600 text-white"
                              : "bg-white hover:bg-gray-100"
                          }`}
                        >
                          {page}
                        </button>
                      ))}

                    <button
                      onClick={() => setCurrentPage(p => Math.min(p + 1, totalPages))}
                      disabled={currentPage === totalPages}
                      className="px-3 py-1 text-sm rounded-md border bg-white hover:bg-gray-100 disabled:opacity-50"
                    >
                      Next
                    </button>
                  </div>
                </div>
                
              </div>
            </div>
            
            
      )}
    {/* <OrderStatusModal
      isOpen={openStatus}
      onClose={() => setOpenStatus(false)}
      order={selectedOrder}
      stages={stageData}
      delivery={deliveryData}
      onSearch={handleModalSearch}
    /> */}
    
    <OrderStatusModal
      isOpen={openStatus}
      onClose={() => setOpenStatus(false)}
      order={selectedOrder}
      stages={stageData}
      delivery={deliveryData}
      searchValue={searchValue}
      setSearchValue={setSearchValue}
      onSearch={handleModalSearch}
    />

    </div>
  );
}

export default Orders;