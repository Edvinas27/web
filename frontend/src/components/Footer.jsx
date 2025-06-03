import React from "react";
import { useState } from "react";

export default function Footer() {

    const [isMenuOpen, setIsMenuOpen] = useState(false);

  const footerSections = [
    { title: "About Us", links: ["Our Story", "Careers", "Press"] },
    { title: "Shop", links: ["New Arrivals", "Best Sellers", "Sale"] },
    { title: "Legal", links: ["Privacy Policy", "Terms of Service", "Cookie Policy"]},
    { title: "Customer Service", links: ["Contact Us", "Returns", "Shipping"] },
  ];

  const socialSelections = [
    { src: "/assets/facebook.png", alt: "Facebook" },
    { src: "/assets/instagram.png", alt: "Instagram" },
    { src: "/assets/tiktok.png", alt: "Tiktok" },
  ]

  return (
    <footer className="bg-white border-t border-gray-200 py-16">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex flex-col md:flex-row justify-between space-y-8 md:space-y-0">
          <div>
            <h1 className="text-5xl tracking-widest font-light mb-4">DROBė</h1>
            <p className="text-xs tracking-widest text-gray-500">
              © 2024 DROBė. All rights reserved.
            </p>
            <div className="mt-4 flex flex-row space-x-4">
              {socialSelections.map((icon, index) => (
                <button key={index}>
                  <img
                    src={icon.src}
                    alt={icon.alt}
                    className="w-8 hover:cursor-pointer hover:scale-110 transition-transform duration-200 ease-in-out"
                  />
                </button>
              ))}
            </div>

            <div className="md:hidden mt-4">
              <button
                onClick={() => setIsMenuOpen(!isMenuOpen)}
                className="text-xs tracking-widest hover:cursor-pointer hover:scale-105 transition-transform duration-200 ease-in-out"
              >
                <span>QUICK LINKS</span>
                <img
                  src="/assets/down-arrow.png"
                  className={`w-4 inline-block ml-1 ${
                    isMenuOpen
                      ? "transform rotate-180 transition-transform duration-200 ease-in-out"
                      : "transform rotate-360 transition-transform duration-200 ease-in-out"
                  }`}
                />
              </button>

              {isMenuOpen && (
                <div className="mt-4 grid grid-cols-2 gap-4 pt-4 pb-4 pr-4">
                  {footerSections.map((section, mobileIndex) => (
                    <div key={mobileIndex}>
                      <h2 className="tracking-widest">{section.title}</h2>
                      <div className="space-y-1">
                        {section.links.map((link, mobileLinkIndex) => (
                          <button key={mobileLinkIndex} className="block">
                            <span className="hover:cursor-pointer hover:scale-105 transition-transform duration-200 ease-in-out text-xs tracking-widest text-gray-500 hover:text-black hover:underline">
                              {link}
                            </span>
                          </button>
                        ))}
                      </div>
                    </div>
                  ))}
                </div>
              )}
            </div>
          </div>

          <div className="hidden md:grid grid-cols-4 gap-4 text-xs tracking-widest pt-4 pb-4 pr-4">
            {footerSections.map((section, index) => (
              <div key={index} className="grid grid-cols-1 gap-1">
                <h2 className="font-bold">{section.title}</h2>
                {section.links.map((link, linkIndex) => (
                  <button
                    key={linkIndex}
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
