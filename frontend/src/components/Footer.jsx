import React from "react";

export default function Footer() {
  return (
    <footer className="bg-white border-t border-gray-200 py-16">
      <div className="max-w-7xl mx-auto px-38 sm:px-40 lg:px-48 ">
        <div className="flex flex-col md:flex-row justify-between space-y-8 md:space-y-0">
          <div>
            <h1 className="text-5xl tracking-widest font-light mb-4">DROBė</h1>
            <p className="text-xs tracking-widest text-gray-500">
              © 2024 DROBė. All rights reserved.
            </p>
            <div className="mt-4 flex flex-row space-x-4">
              {[
                { src: "/assets/facebook.png", alt: "Facebook" },
                { src: "/assets/instagram.png", alt: "Instagram" },
                { src: "/assets/tiktok.png", alt: "Tiktok" },
              ].map((icon, index) => (
                <button key={index}>
                  <img
                    src={icon.src}
                    alt={icon.alt}
                    className="w-8 hover:cursor-pointer hover:scale-110 transition-transform duration-200 ease-in-out"
                  />
                </button>
              ))}
            </div>
          </div>

          <div className="grid grid-cols-4 gap-4 text-xs tracking-widest p-4">
            {[
              { title: "About Us", links: ["Our Story", "Careers", "Press"] },
              {
                title: "Shop", 
                links: ["New Arrivals", "Best Sellers", "Sale"],
              },
              {
                title: "Legal",
                links: ["Privacy Policy", "Terms of Service", "Cookie Policy"],
              },
              {
                title: "Customer Service",
                links: ["Contact Us", "Returns", "Shipping"],
              },
            ].map((section, index) => (
              <div key={index} className="grid grid-cols-1 gap-1">
                <h2 className="font-bold">{section.title}</h2>
                {section.links.map((link, index) => (
                  <button
                    key={index}
                    className="text-gray-500 hover:text-black hover:underline text-left hover:cursor-pointer hover:scale-105 transition-transform duration-200 ease-in-out"
                  >
                    {link}
                  </button>
                ))}
              </div>
            ))}
          </div>
        </div>
      </div>
    </footer>
  );
}
