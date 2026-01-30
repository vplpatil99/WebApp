// import { useEffect, useState ,useMemo} from "react";
// import React from "react";
// import api from "../api/axios";

import { useEffect, useState, useMemo } from "react";
import api from "../api/axios";
import OrderStatusModal from "../components/OrderStatusModal";
import * as XLSX from "xlsx";

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

  const [filterPartyCode, setFilterPartyCode] = useState("");
const [filterMarketing, setFilterMarketing] = useState("");

  // sorting state
  const [sortConfig, setSortConfig] = useState({
    key: null,
    direction: "asc",
  });



  const filteredOrders = useMemo(() => {
  return orders.filter(o => {
    const partyMatch = filterPartyCode
      ? o.party_code?.toLowerCase().includes(filterPartyCode.toLowerCase())
      : true;

    const marketingMatch = filterMarketing
      ? o.marketingPerson?.toLowerCase().includes(filterMarketing.toLowerCase())
      : true;

    return partyMatch && marketingMatch;
  });
}, [orders, filterPartyCode, filterMarketing]);

const partySuggestions = useMemo(() => {
  if (!filterPartyCode) return [];
  return [...new Set(
    orders
      .map(o => o.party_code)
      .filter(p =>
        p && p.toLowerCase().includes(filterPartyCode.toLowerCase())
      )
  )].slice(0, 6);
}, [filterPartyCode, orders]);

const marketingSuggestions = useMemo(() => {
  if (!filterMarketing) return [];
  return [...new Set(
    orders
      .map(o => o.marketingPerson)
      .filter(m =>
        m && m.toLowerCase().includes(filterMarketing.toLowerCase())
      )
  )].slice(0, 6);
}, [filterMarketing, orders]);
  // 🔹 SORT FIRST


  const sortedOrders = useMemo(() => {
  if (!sortConfig.key) return filteredOrders;

  const sorted = [...filteredOrders].sort((a, b) => {
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

    return sortConfig.direction === "asc"
      ? String(aVal).localeCompare(String(bVal))
      : String(bVal).localeCompare(String(aVal));
  });

  return sorted;
}, [filteredOrders, sortConfig]);


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



const exportToExcel = () => {
  if (sortedOrders.length === 0) {
    alert("No data to export");
    return;
  }

  const exportData = sortedOrders.map(o => ({
    "Party Code": o.party_code,
    "Order No": o.order_No,
    "G Order No": o.gOrderNo,
    "Entry Time": o.orderEntryTime
      ? new Date(o.orderEntryTime).toLocaleString()
      : "",
    "Lens Type": o.lens_type,
    "Coat": o.coatColor,
    "Fitting": o.fitting,
    "Type": o.stockorder === "Y" ? "STOCK" : "RX",
    "Current Stage": o.currentStage,
    "L Order No": o.l_OrderNo,
    "Register No": o.registerno,
    "Customer": o.party_cust_code,
    "Marketing Person": o.marketingPerson,
  }));

  const worksheet = XLSX.utils.json_to_sheet(exportData);
  const workbook = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(workbook, worksheet, "Orders");

  XLSX.writeFile(
    workbook,
    `Orders_${fromDate}_to_${toDate}.xlsx`
  );
};




  return (
    <div className="p-6 bg-gray-100 min-h-screen">
      <h1 className="text-2xl font-bold mb-4">Orders</h1>

    {/* <div className="mb-4 flex gap-4 items-end">
      <div>

      </div>

      <div>

      </div>


    </div> */}

      <div className="flex flex-wrap gap-4 mb-3 bg-white p-3 rounded border">

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
        {/* Party Code Filter */}

        <div className="relative">
          <label className="block text-sm font-semibold mb-1">Party Code</label>
          <input
            type="text"
            value={filterPartyCode}
            onChange={(e) => {
              setFilterPartyCode(e.target.value);
              setCurrentPage(1);
            }}
            placeholder="Filter party code"
            className="border px-3 py-2 rounded w-full"
          />

          {partySuggestions.length > 0 &&
            filterPartyCode &&
            !partySuggestions.includes(filterPartyCode) && (
              <div className="absolute z-30 bg-white border rounded shadow w-full mt-1 max-h-40 overflow-y-auto">
                {partySuggestions.map((p, i) => (
                  <div
                    key={i}
                    onMouseDown={() => {
                      setFilterPartyCode(p);
                      setPartySuggestions([]); // 👈 hide dropdown after select
                    }}
                    className="px-3 py-1 text-sm hover:bg-blue-100 cursor-pointer"
                  >
                    {p}
                  </div>
                ))}
              </div>
            )}

        </div>

        {/* Marketing Person Filter */}
        <div className="relative">
          <label className="block text-sm font-semibold mb-1">Marketing Person</label>
          <input
            type="text"
            value={filterMarketing}
            onChange={(e) => {
              setFilterMarketing(e.target.value);
              setCurrentPage(1);
            }}
            placeholder="Filter marketing"
            className="border px-3 py-2 rounded w-full"
          />
          {marketingSuggestions.length > 0 &&
            filterMarketing &&
            !marketingSuggestions.includes(filterMarketing) && (
              <div className="absolute z-30 bg-white border rounded shadow w-full mt-1 max-h-40 overflow-y-auto">
                {marketingSuggestions.map((m, i) => (
                  <div
                    key={i}
                    onMouseDown={() => {
                      setFilterMarketing(m);
                      setMarketingSuggestions([]);
                    }}
                    className="px-3 py-1 text-sm hover:bg-blue-100 cursor-pointer"
                  >
                    {m}
                  </div>
                ))}
              </div>
            )}
        </div>

        <div className="flex items-end">
          <button
            onClick={exportToExcel}
            className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700 h-[42px]"
          >
            Export Excel
          </button>
        </div>

      </div>

      {/* 🔹 Sticky Logo Selector */}
      <div className="z-30 bg-gray-100 pb-3">
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
                      <th className="px-4 py-3">Marketing Person</th>
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
                        <td className="px-4 py-2">{order.marketingPerson || "-"}</td>
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